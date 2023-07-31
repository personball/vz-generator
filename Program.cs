using System.CommandLine;

using vz_generator;
using vz_generator.Commands;
using vz_generator.Initializer;
using vz_generator.Localization;
using vz_generator.Renamer;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand(VzLocales.L(VzLocales.Keys.RootCommandDesc));

        var initCommand = new InitCommand();
        initCommand.SetHandler(InitCommand.InitAsync);
        rootCommand.AddCommand(initCommand);

        var generateCommand = new GenerateCommand();
        generateCommand.AddAlias(VzConsts.GenerateCmd.Alias);
        generateCommand.SetHandler(generateCommand.GenerateAsync);
        rootCommand.AddCommand(generateCommand);

        var renameCommand = new RenameCommand();
        renameCommand.AddAlias(VzConsts.RenameCmd.Alias);
        renameCommand.SetHandler(renameCommand.RenameAsync);
        rootCommand.AddCommand(renameCommand);

        return await rootCommand.InvokeAsync(args);
    }
}