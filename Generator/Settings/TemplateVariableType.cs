using System.Text.Json.Serialization;

namespace vz_generator.Commands.Settings;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TemplateVariableType
{
    String = 0,

    JsonFile = 1,

    YamlFile = 2,
    // JsonFileUri 
}
// TODO: load json from uri
