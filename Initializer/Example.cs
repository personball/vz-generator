using vz_generator.Commands.Settings;

namespace vz_generator.Initializer;

public class Example
{
    public string Name { get; set; }

    public List<GeneratorSetting> Settings { get; set; } = new List<GeneratorSetting>();
}