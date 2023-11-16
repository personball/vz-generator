using System.CommandLine.Invocation;
using vz_generator.Commands.Settings;

namespace vz_generator.Generator.Settings.SettingResolvers;
public class ResolveContext
{
    public ResolveContext(InvocationContext context)
    {
        InvocationContext = context;
    }

    public InvocationContext InvocationContext { get; }

    public FileInfo? SettingFilePath { get; set; }

    public List<GeneratorSetting>? FileSettings { get; set; }

    public string? SelectedOption { get; set; }

    public GeneratorSetting? Result { get; set; } = null;
}