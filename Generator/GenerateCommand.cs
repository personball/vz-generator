using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;

using vz_generator.Commands.Settings;
using vz_generator.Generator.Liquid;
using vz_generator.Generator.Settings.SettingResolvers;
using vz_generator.Localization;

namespace vz_generator.Commands;

public sealed class GenerateCommand : Command
{
    public GenerateCommand() : base(VzConsts.GenerateCmd.Name, VzLocales.L(VzLocales.Keys.GenerateCommandDesc))
    {
        foreach (var opt in Opts())
        {
            AddOption(opt);
        }
    }

    /// <summary>
    /// options to override settings
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<Option> Opts()
    {
        yield return ConfigOpt;
        yield return SelectOpt;
        yield return TplPathOpt;
        // yield return SyntaxOpt;
        yield return VarStringOpt;
        yield return VarJsonFileOpt;
        yield return VarYamlFileOpt;
        yield return OutputOpt;
        yield return OverrideOpt;
        yield return WatchOpt;
    }
    public static Option<FileInfo> ConfigOpt = new(
        aliases: new string[] { "--config", "-c" },
        description: VzLocales.L(VzLocales.Keys.GOptConfigOptDesc));

    public static Option<FileSystemInfo> OutputOpt = new(
        aliases: new string[] { "--output", "-o" },
        description: VzLocales.L(VzLocales.Keys.GOptOutputOptDesc)
    );

    public static Option<bool?> OverrideOpt = new(
        name: "--override",
        description: VzLocales.L(VzLocales.Keys.GOptOverrideOptDesc),
        getDefaultValue: () => (bool?)null
    );

    public static Option<Dictionary<string, string>> VarStringOpt = new(
        aliases: new string[] { "--var" },
        description: VzLocales.L(VzLocales.Keys.GOptVarStringOptDesc),
        parseArgument: result => result.Tokens.Select(t => t.Value.Split('=')).ToDictionary(p => p[0], p => p[1])
    );

    public static Option<Dictionary<string, FileInfo>> VarJsonFileOpt = new(
        aliases: new string[] { "--var-json-file" },
        description: VzLocales.L(VzLocales.Keys.GOptVarJsonFileOptDesc),
        parseArgument: result => result.Tokens.Select(t => t.Value.Split('=')).ToDictionary(p => p[0], p => new FileInfo(p[1]))
    );

    public static Option<Dictionary<string, FileInfo>> VarYamlFileOpt = new(
        aliases: new string[] { "--var-yaml-file" },
        description: VzLocales.L(VzLocales.Keys.GOptVarYamlFileOptDesc),
        parseArgument: result => result.Tokens.Select(t => t.Value.Split('=')).ToDictionary(p => p[0], p => new FileInfo(p[1]))
    );

    public static Option<FileSystemInfo> TplPathOpt = new(
        aliases: new string[] { "--template", "-t" },
        description: VzLocales.L(VzLocales.Keys.GOptTplPathOptDesc)
    );
    // TODO: 当指定 argument 作为模板内容时，忽略本选项。

    public static Option<TemplateSyntax> SyntaxOpt = new(
        aliases: new string[] { "--syntax", "-s" },
        description: VzLocales.L(VzLocales.Keys.GOptSyntaxOptDesc),
        getDefaultValue: () => TemplateSyntax.Liquid
    );

    public static Option<string> SelectOpt = new(
        aliases: new string[] { "--option", "-p" },
        description: VzLocales.L(VzLocales.Keys.GOptSelectOptDesc)
    );

    public static Option<bool> WatchOpt = new(
        aliases: new string[] { "--watch", "-w" },
        description: VzLocales.L(VzLocales.Keys.GOptWatchOptDesc),
        getDefaultValue: () => false
    );

    // TODO: 完全忽略配置，不指定option，从标准输入(argument.0)接收模板内容（仅支持单个文件）

    private static FileSystemWatcher? TemplateWatcher;

    public async Task GenerateAsync(InvocationContext context)
    {
        var watched = context.ParseResult.GetValueForOption(WatchOpt);

        // TODO: settings hot reload?
        var setting = await LoadSettings(context);

        if (watched)
        {
            if (!File.Exists(setting.TemplatePath) && !Directory.Exists(setting.TemplatePath))
            {
#if DEBUG
                Console.WriteLine($"Watch {nameof(setting.TemplatePath)} Fail: {setting.TemplatePath} Not Exists!{Environment.NewLine}");
#else
                context.Console.Error.Write(
                    VzLocales.L(
                        VzLocales.Keys.GFailedErrorResult, 
                        $"Watch {nameof(setting.TemplatePath)} Fail: {setting.TemplatePath} Not Exists!", "", Environment.NewLine));
                context.ExitCode = 2;
#endif
                return;
            }

            // keep running, timer
            var watchPath = Path.GetDirectoryName(setting.TemplatePath);
            context.Console.Write($"Start watching {watchPath} ...{Environment.NewLine}");
            TemplateWatcher = new FileSystemWatcher(watchPath!)
            {
                NotifyFilter = NotifyFilters.Attributes
                                | NotifyFilters.CreationTime
                                | NotifyFilters.DirectoryName
                                | NotifyFilters.FileName
                                | NotifyFilters.LastAccess
                                | NotifyFilters.LastWrite
                                | NotifyFilters.Security
                                | NotifyFilters.Size
            };

            TemplateWatcher.Changed += OnChanged;
            TemplateWatcher.Created += OnChanged;
            TemplateWatcher.Deleted += OnChanged;
            TemplateWatcher.Renamed += OnChanged;
            TemplateWatcher.Error += OnError;

            TemplateWatcher.Filter = "";
            TemplateWatcher.IncludeSubdirectories = true;
            TemplateWatcher.EnableRaisingEvents = true;

            async void OnChanged(object sender, FileSystemEventArgs e)
            {
                context.Console.Write($"{e.FullPath} {e.ChangeType}. Re-generating...");
                var stopWatch = Stopwatch.StartNew();
                stopWatch.Start();
                await ExecuteAsync(setting, context);
                stopWatch.Stop();
                context.Console.Write($"completed in {stopWatch.ElapsedMilliseconds}ms.{Environment.NewLine}");
            }

            void OnError(object sender, ErrorEventArgs e)
            {
                var ex = e.GetException();
#if DEBUG
                Console.WriteLine($"Generate Fail: {ex.Message}{Environment.NewLine}");
#else
                context.Console.Error.Write(
                    VzLocales.L(
                        VzLocales.Keys.GFailedErrorResult, ex.Message, Environment.NewLine, ex.StackTrace));
                context.ExitCode = 2;
#endif
            }
        }

        await ExecuteAsync(setting, context);

        if (watched)
        {
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

            TemplateWatcher?.Dispose();
        }
    }

    private async Task<GeneratorSetting> LoadSettings(InvocationContext context)
    {
        var resolver =
        new CliPromptGeneratorSettingResolver(
            new CliOptionGeneratorSettingDecorator(
                new DefaultGeneratorSettingResolver()));

        var ctx = new ResolveContext(context);

        await resolver.ResolveAsync(ctx);

        return ctx.Result!;
    }

    private async Task ExecuteAsync(GeneratorSetting setting, InvocationContext context)
    {
        try
        {
            if (setting.TemplateSyntax == TemplateSyntax.Liquid)
            {
                var executor = new LiquidTemplateExecutor(setting, context);
                await executor.ExecuteAsync();
                return;
            }

            throw new NotImplementedException();
        }
        catch (System.Exception ex)
        {
#if DEBUG
            Console.WriteLine($"Generate Fail: {ex.Message}{Environment.NewLine}");
#else
            context.Console.Error.Write(
                VzLocales.L(
                    VzLocales.Keys.GFailedErrorResult, ex.Message, Environment.NewLine, ex.StackTrace));
            context.ExitCode = 2;
#endif
        }
    }
}
