using System.CommandLine;
using System.CommandLine.Invocation;

using vz_generator.Commands.Settings;
using vz_generator.Generator.Liquid;
using vz_generator.Generator.Settings.SettingResolvers;
using vz_generator.Localization;

namespace vz_generator.Commands;

public sealed class GenerateCommand : Command
{
    public GenerateCommand() : base(VzConsts.GenerateCmd.Name, VzLocales.L(VzLocales.Keys.GenerateCommandDesc))
    {
        foreach (var opt in Options())
        {
            AddOption(opt);
        }
    }

    /// <summary>
    /// options to override settings
    /// </summary>
    /// <returns></returns>
    protected static IEnumerable<Option> Options()
    {
        yield return ConfigOpt;
        yield return SelectOpt;
        yield return TplPathOpt;
        // yield return SyntaxOpt;
        yield return VarStringOpt;
        yield return VarJsonFileOpt;
        yield return OutputOpt;
        yield return OverrideOpt;
    }
    public static Option<FileInfo> ConfigOpt = new Option<FileInfo>(
        aliases: new string[] { "--config", "-c" },
        description: VzLocales.L(VzLocales.Keys.GOptConfigOptDesc));

    public static Option<FileSystemInfo> OutputOpt = new Option<FileSystemInfo>(
        aliases: new string[] { "--output", "-o" },
        description: VzLocales.L(VzLocales.Keys.GOptOutputOptDesc)
    );

    public static Option<bool?> OverrideOpt = new Option<bool?>(
        name: "--override",
        description: VzLocales.L(VzLocales.Keys.GOptOverrideOptDesc),
        getDefaultValue: () => (bool?)null
    );

    public static Option<Dictionary<string, string>> VarStringOpt = new Option<Dictionary<string, string>>(
        aliases: new string[] { "--var" },
        description: VzLocales.L(VzLocales.Keys.GOptVarStringOptDesc),
        parseArgument: result => result.Tokens.Select(t => t.Value.Split('=')).ToDictionary(p => p[0], p => p[1])
    );

    public static Option<Dictionary<string, FileInfo>> VarJsonFileOpt = new Option<Dictionary<string, FileInfo>>(
        aliases: new string[] { "--var-json-file" },
        description: VzLocales.L(VzLocales.Keys.GOptVarJsonFileOptDesc),
        parseArgument: result => result.Tokens.Select(t => t.Value.Split('=')).ToDictionary(p => p[0], p => new FileInfo(p[1]))
    );

    public static Option<FileSystemInfo> TplPathOpt = new Option<FileSystemInfo>(
        aliases: new string[] { "--template", "-t" },
        description: VzLocales.L(VzLocales.Keys.GOptTplPathOptDesc)
    );
    // TODO: 当指定 argument 作为模板内容时，忽略本选项。

    public static Option<TemplateSyntax> SyntaxOpt = new Option<TemplateSyntax>(
        aliases: new string[] { "--syntax", "-s" },
        description: VzLocales.L(VzLocales.Keys.GOptSyntaxOptDesc)
    );

    public static Option<string> SelectOpt = new Option<string>(
        aliases: new string[] { "--option", "-p" },
        description: VzLocales.L(VzLocales.Keys.GOptSelectOptDesc)
    );

    // TODO: 完全忽略配置，不指定option，从标准输入(argument.0)接收模板内容（仅支持单个文件）

    public async Task GenerateAsync(InvocationContext context)
    {
        try
        {
            var setting = await LoadSettings(context);
            await ExecuteAsync(setting, context);
        }
        catch (System.Exception ex)
        {
#if DEBUG
            Console.WriteLine($"Generate Fail: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
#endif
            context.Console.Error.Write($"Generate Fail: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
            context.ExitCode = 2;
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
        if (setting.TemplateSyntax == TemplateSyntax.Liquid)
        {
            var executor = new LiquidTemplateExecutor(setting, context);
            await executor.ExecuteAsync();
            return;
        }

        throw new NotImplementedException();
    }
}
