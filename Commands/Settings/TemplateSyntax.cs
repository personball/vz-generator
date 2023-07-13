using System.Text.Json.Serialization;

namespace vz_generator.Commands.Settings;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TemplateSyntax
{
    Liquid = 0,
    Razor = 1
}