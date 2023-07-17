using System.Text;
using System.Text.Json;
using Json.Schema;
using Json.Schema.Generation;
using vz_generator.Commands.Settings;
using vz_generator.Initializer.JsonSchemas.VsCode;

namespace vz_generator.Initializer.JsonSchemas;
public static class SettingSchemas
{
    public static async Task InitAsync(string configRoot)
    {
        // ./.vz/generate.settings.schema.json
        var generate_schema_path = Path.Combine(configRoot, VzConsts.GenerateCmd.SettingSchemaFileName);
        if (!File.Exists(generate_schema_path))
        {
            var schema = new JsonSchemaBuilder()
                .FromType<List<GeneratorSetting>>(
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
    }
    public static async Task SetupVsCodeAsync(string vscodePath)
    {
        // .vscode/settings.json
        var vscode_settings_json = Path.Combine(vscodePath, VsCodeSettings.FileName);
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
        // TODO: 探查 .vscode/settings.json 配置内容，如果未设置则修改配置
        if (!File.Exists(vscode_settings_json))
        {
            await File.WriteAllTextAsync(vscode_settings_json, vscode_json_schema_rule);
        }
        else
        {
            Console.WriteLine($"Please edit .vscode{Path.DirectorySeparatorChar}settings.json with follow content:");
            Console.Write(vscode_json_schema_rule);
            Console.WriteLine();
        }
    }
}