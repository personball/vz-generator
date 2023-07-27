using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using Json.Schema;

using vz_generator.Commands.Settings;
using vz_generator.Initializer.JsonSchemas;
using vz_generator.Initializer.JsonSchemas.VsCode;
using vz_generator.Localization;

namespace vz_generator.Initializer;

public sealed class InitCommand : Command
{
    public InitCommand()
        : base(VzConsts.InitCmd.Name, VzLocales.L(VzLocales.Keys.InitCommandDesc))
    {
        foreach (var opt in Options())
        {
            AddOption(opt);
        }
    }

    /// <summary>
    /// option to specify samples to init
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Option> Options()
    {
        yield return withSample;
        yield return listSamples;
    }

    private static Option<bool> listSamples = new Option<bool>(
        name: "--list-samples",
        description: VzLocales.L(VzLocales.Keys.InitOptListSampleDesc),
        getDefaultValue: () => false);
    private static Option<string[]> withSample = new Option<string[]>(
        name: "--with-sample",
        description: VzLocales.L(VzLocales.Keys.InitOptWithSampleDesc));

    public async Task InitAsync(InvocationContext context)
    {
        var doList = context.ParseResult.GetValueForOption(listSamples);
        if (doList)
        {
            foreach (var item in SamplesCollector.GetExamples())
            {
                context.Console.Out.Write($"{item.Name}{Environment.NewLine}");
            }

            return;
        }

        var currentPath = Environment.CurrentDirectory;
        // 创建 .vz 目录
        var vzRoot = CreateDirectoryIfNotExists(currentPath, VzConsts.ConfigRoot);
        // 创建 templates 目录
        var templatesRoot = CreateDirectoryIfNotExists(vzRoot, VzConsts.TemplateRoot);
        // 创建 generate.settings.schema.json
        await SettingSchemas.InitAsync(vzRoot);
        // 设置 .vscode/settings.json 添加 generate.settings.json 的json schemas 校验
        var vscodePath = CreateDirectoryIfNotExists(currentPath, VsCodeSettings.ConfigPath);
        await SettingSchemas.SetupVsCodeAsync(vscodePath, context);
        // 按需导出 samples （导出模板到 templates samples 目录下，同时添加相应的示例 option 到 generate.settings.json）
        var withSamples = context.ParseResult.GetValueForOption(withSample);
        var examples = SamplesCollector.GetExamples();
        if (withSamples != null && withSamples.Any())
        {
            examples = examples.Where(e => withSamples.Contains(e.Name)).ToList();
        }
        // ./.vz/templates/samples/
        var sample_root = CreateDirectoryIfNotExists(templatesRoot, VzConsts.SampleRoot);
        // export sample templates
        await SampleTemplatesExtractor.ExportAsync(sample_root, examples.Select(e => e.Name).ToArray());

        // 创建 generate.settings.json
        var generate_setting_path = Path.Combine(vzRoot, VzConsts.GenerateCmd.SettingFileName);
        var sampleSettings = new List<GeneratorSetting>();
        if (File.Exists(generate_setting_path))
        {
            try
            {
                sampleSettings = JsonSerializer.Deserialize<List<GeneratorSetting>>(
                                await File.ReadAllTextAsync(generate_setting_path),
                                new JsonSerializerOptions(JsonSerializerDefaults.Web));
            }
            catch (JsonException ex)
            {
                context.Console.Error.Write(
                    VzLocales.L(VzLocales.Keys.JsonDeserializeFailError, generate_setting_path, ex.Message));
                context.ExitCode = 2;
                return;
            }

            if (sampleSettings == null)
            {
                sampleSettings = new List<GeneratorSetting>();
            }
        }

        foreach (var item in examples)
        {
            foreach (var setting in item.Settings)
            {
                if (sampleSettings.Any(s => s.Option == setting.Option))
                {
                    context.Console.Out.Write(
                        VzLocales.L(VzLocales.Keys.InitSettingOptionExistsSkipWarn, setting.Option, item.Name)
                    );
                    continue;
                }

                sampleSettings.Add(setting);
            }
        }

        await File.WriteAllTextAsync(
            generate_setting_path,
            JsonSerializer.Serialize(
                sampleSettings,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault
                }),
            Encoding.UTF8);
    }

    private string CreateDirectoryIfNotExists(params string[] paths)
    {
        var path = Path.Combine(paths);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
    }
}
