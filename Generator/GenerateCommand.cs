using System.CommandLine;
using System.CommandLine.Invocation;
using vz_generator.Commands.Settings;

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
        yield return new Option<int>(
            aliases: new string[] { "--delay", "-d" },
            description: "Delay between lines, specified as milliseconds per character in a line.",
            getDefaultValue: () => 42);
        yield return new Option<ConsoleColor>(
            aliases: new string[] { "--fgcolor", "-f" },
            description: "Foreground color of text displayed on the console.",
            getDefaultValue: () => ConsoleColor.White);
    }

    public async Task GenerateAsync(InvocationContext context)
    {
        context.Console.Out.Write($"Hello world");
    }

    private static async Task<List<GeneratorSetting>> LoadSettings()
    {


        return new List<GeneratorSetting>();
    }
}
