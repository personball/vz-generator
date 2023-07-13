using System.CommandLine;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Json.Schema;
using Json.Schema.Generation;
using Sharprompt;
using vz_generator;
using vz_generator.Commands.Settings;

class Program
{
    static async Task<int> Main(string[] args)
    {
        // var fileOption = new Option<FileInfo?>(
        //     name: "--file",
        //     description: "The file to read and display on the console.");

        await InitAsync();

        var rootCommand = new RootCommand("CLI Tools for VZeroSoft");
        // rootCommand.AddOption(fileOption);

        rootCommand.SetHandler(async (ctx) =>
        {
            var executablePath = Environment.GetCommandLineArgs()[0];
            var currentPath = Environment.CurrentDirectory;

            ctx.Console.WriteLine($"{nameof(executablePath)}:{executablePath}");
            ctx.Console.WriteLine($"{nameof(currentPath)}:{currentPath}");

            // read file in config



            // var file = ctx.ParseResult.GetValueForOption(fileOption);
            // var token = ctx.GetCancellationToken();

            //ReadFile(file!); 
        });

        return await rootCommand.InvokeAsync(args);
    }

    private static async Task InitAsync()
    {
        // ./.vz/
        var configRoot = Path.Combine(Environment.CurrentDirectory, VzConsts.ConfigRoot);
        if (!Directory.Exists(configRoot))
        {
            Directory.CreateDirectory(configRoot);
        }

        // ./.vz/generate.settings.schema.json
        var generate_schema_path = Path.Combine(configRoot, VzConsts.GenerateCmd.SettingSchemaFileName);
        if (!File.Exists(generate_schema_path))
        {
            var schema = new JsonSchemaBuilder()
                .FromType<List<GenerateSetting>>(
                    new SchemaGeneratorConfiguration
                    {
                        PropertyNamingMethod = PropertyNamingMethods.CamelCase
                    })
                .Build();

            await File.WriteAllTextAsync(
                generate_schema_path,
                JsonSerializer.Serialize(
                    schema,
                    new JsonSerializerOptions(JsonSerializerDefaults.Web)
                    {
                        WriteIndented = true
                    }),
                Encoding.UTF8);
        }

        // ./.vz/generate.settings.json
        var generate_setting_path = Path.Combine(configRoot, VzConsts.GenerateCmd.SettingFileName);
        if (!File.Exists(generate_setting_path))
        {
            // sample settings
            var setting = new GenerateSetting
            {
                Option = "Create Pinia",
                TemplatePath = Path.Combine(
                    ".",
                    VzConsts.ConfigRoot,
                    VzConsts.TemplateRoot,
                    "samples",
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
                    new List<GenerateSetting> { setting },
                    new JsonSerializerOptions(JsonSerializerDefaults.Web)
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault
                    }),
                Encoding.UTF8);
        }

        // ./.vz/templates/samples/
        var sample_root = Path.Combine(configRoot, "templates", "samples");
        if (!Directory.Exists(sample_root))
        {
            Directory.CreateDirectory(sample_root);
        }
        // ./.vz/templates/samples/pinia/{{store}}.js
        var sample_store_js = Path.Combine(sample_root, "pinia");
        if (!Directory.Exists(sample_store_js))
        {
            Directory.CreateDirectory(sample_store_js);
        }

        sample_store_js = Path.Combine(sample_store_js, "{{store}}.js");
        if (!File.Exists(sample_store_js))
        {
            var sample_store_js_content = """
import { defineStore } from 'pinia'
import { get{{model}} } from '@/api/{{store}}(camelCase)'

export const use{{store}} = defineStore('{{store}}(camelCase)', {
    state: () => ({
      {{model}}(camelCase): null
    }),
    actions: {
      clear{{model}}() {
        this.{{model}}(camelCase) = null
      },
      
      async get{{model}}() {
        const res = await get{{model}}()
        if (res) {
          this.{{model}}(camelCase) = res
        } 
        // else {
        //   this.{{model}}(camelCase) = { name: 'SomeThingName' }
        // }
      },
    },
  })
""";
            await File.WriteAllTextAsync(sample_store_js, sample_store_js_content, Encoding.UTF8);
        }

        // .vscode
        var vscode_config_root = Path.Combine(Environment.CurrentDirectory, ".vscode");
        if (!Directory.Exists(vscode_config_root))
        {
            Directory.CreateDirectory(vscode_config_root);
        }
        // .vscode/settings.json 存在就不改，提示用户自己编辑
        var vscode_settings_json = Path.Combine(vscode_config_root, "settings.json");
        var vscode_json_schema_rule = """
{
    "json.schemas":[
        {
            "fileMatch": [
                ".vz/generate.settings.json"
            ],
            "url": "./.vz/generate.settings.schema.json"
        }
    ]
}
""";
        if (!File.Exists(vscode_settings_json))
        {
            await File.WriteAllTextAsync(vscode_settings_json, vscode_json_schema_rule);
        }
        else
        {
            Console.WriteLine($"Please edit .vscode{Path.DirectorySeparatorChar}settings.json with follow content:");
            Console.Write(vscode_json_schema_rule);
            Console.WriteLine();
            //  Prompt.Select();
        }

    }

    static void ReadFile(FileInfo file)
    {
        File.ReadLines(file.FullName).ToList()
            .ForEach(line => Console.WriteLine(line));
    }
}
// 2 subCommands
// vz init  :初始化配置目录，以及示例模版
// vz g     :执行模板化文件生成，如果没有 选项，则按配置文件执行，如果配置不足，则进行交互式提示并获取输入 
// .vz/templates/**
// .vz/generate.config.json
// default config value | config parse | option parse, options override config, 
// 
// TODO: 在执行命令的当前路径(var currentPath= Environment.CurrentDirectory;)下寻找 .vz 目录, 如果不存在，则提示初始化


// Use Case:
// 1. 根据指定的单个文件（或目录中多个）模版（liquid语法），结合指定的json对象（作为liquid变量），生成文件；
// 1.2 文件名和路径名都可以支持变量替换；
// 1.3 在 .vz 目录下可以有一个配置文件，指定多个固定的选项，对应各自的一套配置；
// 1.4 文件生成所需要的配置，如果未配齐全，则可以通过命令行交互的方式进行配置补充；
// 1.5 对于 .vz 目录下的预定义配置，可以通过命令选项进行配置覆盖，可以通过选项提供命令行交互可能涉及的所有配置
// CLI 非功能性需求：
// a. 执行 generate 子命令前，检查当前目录中是否存在.vz目录，如果没有，则终止执行，并提示用户执行 init 子命令进行初始化
// b. init 子命令，生成实例配置以及示例模版，其中示例模版按各种开发框架进行区分
// c. 模板引擎可以切换，以支持不同的模板语法
// d. 兼容 __name__(camelCase) 语法，预处理为 {{ name | camelCase}}
// e. 自定义指令 directive