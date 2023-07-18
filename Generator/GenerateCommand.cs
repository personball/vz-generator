using System.CommandLine;
using System.CommandLine.Invocation;
using vz_generator.Commands.Settings;
using vz_generator.Generator.Liquid;
using vz_generator.Generator.Settings.SettingResolvers;

namespace vz_generator.Commands;

public sealed class GenerateCommand : Command
{
    public GenerateCommand() : base(VzConsts.GenerateCmd.Name, "Generate Files With Specific Templates.")
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
    }
    public static Option<FileInfo> ConfigOpt = new Option<FileInfo>(
        aliases: new string[] { "--config", "-c" },
        description: "加载指定配置文件。");

    public static Option<FileSystemInfo> OutputOpt = new Option<FileSystemInfo>(
        aliases: new string[] { "--output", "-o" },
        description: "（可选）输出结果到指定位置。未指定则按配置文件执行，配置文件中未配置，则默认输出到./output。"
    );

    public static Option<Dictionary<string, string>> VarStringOpt = new Option<Dictionary<string, string>>(
        aliases: new string[] { "--vars" },
        description: "（可选）指定变量键值, --vars key1=val1 key2=val2 可以覆盖配置文件中的默认配置，若未完全覆盖，则会有交互提示输入。",
        parseArgument: result => result.Tokens.Select(t => t.Value.Split('=')).ToDictionary(p => p[0], p => p[1])
    );

    public static Option<Dictionary<string, FileInfo>> VarJsonFileOpt = new Option<Dictionary<string, FileInfo>>(
        aliases: new string[] { "--var-json-files" },
        description: "（可选）指定变量键值来自一个json文件， --var-json-files key1=./path/to/jsonfile key2=./path/to/jsonfile2 可以覆盖配置文件中的默认配置，若未完全覆盖，则会有交互提示输入。",
        parseArgument: result => result.Tokens.Select(t => t.Value.Split('=')).ToDictionary(p => p[0], p => new FileInfo(p[1]))
    );

    public static Option<FileSystemInfo> TplPathOpt = new Option<FileSystemInfo>(
        aliases: new string[] { "--template", "-t" },
        description: "（可选）指定模板文件位置，可以是一个目录也可以是一个文件。当指定 argument 作为模板内容时，忽略本选项。"
    );

    public static Option<TemplateSyntax> SyntaxOpt = new Option<TemplateSyntax>(
        aliases: new string[] { "--syntax", "-s" },
        description: "指定模板语法，默认为 Liquid, 其他模板引擎待开发。"
    );

    public static Option<string> SelectOpt = new Option<string>(
        aliases: new string[] { "--option", "-p" },
        description: "（可选）指定执行哪套配置，如果配置中的 option 带有空格，则此处需要引号。"
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
            Console.WriteLine($"Generate Fail: {ex.Message}");
#endif
            context.Console.Error.Write($"Generate Fail: {ex.Message} \n");
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
