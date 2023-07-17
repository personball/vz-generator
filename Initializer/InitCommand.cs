using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Json.Schema;
using vz_generator.Commands.Settings;
using vz_generator.Initializer.JsonSchemas;
using vz_generator.Initializer.JsonSchemas.VsCode;

namespace vz_generator.Initializer;

public sealed class InitCommand : Command
{
    public InitCommand()
        : base(VzConsts.InitCmd.Name, "Init Settings And Templates.")
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
        description: "列出所有可用示例名称。",
        getDefaultValue: () => false);
    private static Option<string[]> withSample = new Option<string[]>(
        name: "--with-sample",
        description: """
        --with-sample abp --with-sample vue 可多次指定需要导出的示例名称。
        不指定则默认导出全部，可以通过 --list-samples 查看所有可用示例名称。
        """);

    public async Task InitAsync(InvocationContext context)
    {
        var doList = context.ParseResult.GetValueForOption(listSamples);
        if (doList)
        {
            foreach (var item in SamplesCollector.GetExamples())
            {
                context.Console.Out.Write($"{item.Name}\n");
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
        await SettingSchemas.SetupVsCodeAsync(vscodePath);
        // 按需导出 samples （导出模板到 templates samples 目录下，同时添加相应的示例 option 到 generate.settings.json）
        var withSamples = context.ParseResult.GetValueForOption(withSample);
        var examples = SamplesCollector.GetExamples();
        if (withSamples != null && withSamples.Any())
        {
            examples = examples.Where(e => withSamples.Contains(e.Name)).ToList();
        }
        // ./.vz/templates/samples/
        var sample_root = CreateDirectoryIfNotExists(templatesRoot, VzConsts.SampleRoot);
        // TODO: export sample templates
        foreach (var item in examples)
        {
            // todo CreateIfNotExists
            context.Console.Out.Write($"TODO:export {item.Name} sample templates from assembly...");
        }

        // TODO: 创建 generate.settings.json
        var generate_setting_path = Path.Combine(vzRoot, VzConsts.GenerateCmd.SettingFileName);
        var sampleSettings = new List<GeneratorSetting>();
        if (File.Exists(generate_setting_path))
        {
            try
            {
                sampleSettings = JsonSerializer.Deserialize<List<GeneratorSetting>>(
                                await File.ReadAllTextAsync(generate_setting_path));
            }
            catch (JsonException ex)
            {
                context.Console.Error.Write($"Can't load {generate_setting_path}, Json Deserialize Failed: {ex.Message}");
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
            if (sampleSettings.Any(s => s.Option == item.Setting.Option))
            {
                context.Console.Out.Write($"WARN: option <{item.Setting.Option}> from sample {item.Name} already exists, settings skipped...\n");
                continue;
            }

            sampleSettings.Add(item.Setting);
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
        //         // ./.vz/templates/samples/pinia/{{store}}.js
        //         var sample_store_js = Path.Combine(sample_root, "pinia");
        //         if (!Directory.Exists(sample_store_js))
        //         {
        //             Directory.CreateDirectory(sample_store_js);
        //         }

        //         sample_store_js = Path.Combine(sample_store_js, "{{store}}.js");
        //         if (!File.Exists(sample_store_js))
        //         {
        //             var sample_store_js_content = """
        // import { defineStore } from 'pinia'
        // import { get{{model}} } from '@/api/{{store}}(camelCase)'

        // export const use{{store}} = defineStore('{{store}}(camelCase)', {
        //     state: () => ({
        //       {{model}}(camelCase): null
        //     }),
        //     actions: {
        //       clear{{model}}() {
        //         this.{{model}}(camelCase) = null
        //       },

        //       async get{{model}}() {
        //         const res = await get{{model}}()
        //         if (res) {
        //           this.{{model}}(camelCase) = res
        //         } 
        //         // else {
        //         //   this.{{model}}(camelCase) = { name: 'SomeThingName' }
        //         // }
        //       },
        //     },
        //   })
        // """;
        //             await File.WriteAllTextAsync(sample_store_js, sample_store_js_content, Encoding.UTF8);
        //}


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


    public static async Task InitSampleSettingsAsync(string configRoot)
    {
        // ./.vz/generate.settings.json
        var generate_setting_path = Path.Combine(configRoot, VzConsts.GenerateCmd.SettingFileName);
        if (!File.Exists(generate_setting_path))
        {
            // sample settings
            var setting = new GeneratorSetting
            {
                Option = "Create Pinia",
                TemplatePath = Path.Combine(
                    ".",
                    VzConsts.ConfigRoot,
                    VzConsts.TemplateRoot,
                    VzConsts.SampleRoot,
                    "pinia",
                    "{{store}}.js"
                ),
                Output = Path.Combine(
                    ".",
                    "src",
                    "pinia",
                    "modules",
                    "{{store|camelCase}}.js")
            };

            setting.Variables.Add(new TemplateVariable { Name = "store", Type = TemplateVariableType.String });
            setting.Variables.Add(new TemplateVariable { Name = "model", Type = TemplateVariableType.String });

            await File.WriteAllTextAsync(
                generate_setting_path,
                JsonSerializer.Serialize(
                    new List<GeneratorSetting> { setting },
                    new JsonSerializerOptions(JsonSerializerDefaults.Web)
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault
                    }),
                Encoding.UTF8);
        }
    }


}
