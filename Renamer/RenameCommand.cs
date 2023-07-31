using System.CommandLine;
using System.CommandLine.Invocation;

using Sharprompt;

using vz_generator.Localization;

namespace vz_generator.Renamer;

public sealed class RenameCommand : Command
{
    public RenameCommand()
        : base(VzConsts.RenameCmd.Name, VzLocales.L(VzLocales.Keys.RenameCommandDesc))
    {
        AddArgument(renameTargetArg);

        foreach (var opt in Options())
        {
            AddOption(opt);
        }
    }

    private static Argument<FileSystemInfo> renameTargetArg = new Argument<FileSystemInfo>(
        name: "target",
        description: VzLocales.L(VzLocales.Keys.RTargetArgDesc)
    );

    public static IEnumerable<Option> Options()
    {
        yield return SkipContentOpt;
        yield return ReplacePairsOpt;
        yield return OutputOpt;
        yield return OverrideOpt;
    }

    private static readonly Option<bool> SkipContentOpt = new(
        name: "--skip-content",
        description: VzLocales.L(VzLocales.Keys.ROptSkipContentOptDesc),
        getDefaultValue: () => false
    );

    private static readonly Option<Dictionary<string, string>> ReplacePairsOpt = new(
        aliases: new string[] { "-r", "--replace" },
        description: VzLocales.L(VzLocales.Keys.ROptReplacePairsOptDesc),
        parseArgument: result => result.Tokens.Select(t => t.Value.Split('=')).ToDictionary(p => p[0], p => p[1]));

    private static readonly Option<DirectoryInfo> OutputOpt = new(
        aliases: new string[] { "-o", "--output" },
        description: VzLocales.L(VzLocales.Keys.ROptOutputOptDesc),
        getDefaultValue: () => new DirectoryInfo(".")
    );

    private static readonly Option<bool?> OverrideOpt = new(
        name: "--override",
        description: VzLocales.L(VzLocales.Keys.ROptOverrideOptDesc),
        getDefaultValue: () => (bool?)null
    );

    // TODO: --no-copy? 需要文件系统事务性操作支持。
    public async Task RenameAsync(InvocationContext context)
    {
        try
        {
            await RenameInternalAsync(context);
        }
        catch (System.Exception ex)
        {
#if DEBUG
            Console.WriteLine($"Rename Fail:{ex.Message},{Environment.NewLine}");
#else
            context.Console.Error.Write(
                    VzLocales.L(
                        VzLocales.Keys.RenameFailedErrorResult, ex.Message, Environment.NewLine, ex.StackTrace));
            context.ExitCode = 2;
#endif
        }
    }

    private async Task RenameInternalAsync(InvocationContext context)
    {
        var target = context.ParseResult.GetValueForArgument(renameTargetArg);
        if (!target.Exists)
        {
            throw new ArgumentNullException(
                nameof(target),
                VzLocales.L(VzLocales.Keys.RenameTargetNotFoundPrompt, target.FullName));
        }

        var replacers = context.ParseResult.GetValueForOption(ReplacePairsOpt);
        if (replacers == null || !replacers.Any())
        {
            context.Console.Out.Write(VzLocales.L(VzLocales.Keys.RenameReplacePairsOptNotFoundPrompt));
            return;
        }

        var output = context.ParseResult.GetValueForOption(OutputOpt);
        var skipContent = context.ParseResult.GetValueForOption(SkipContentOpt);
        var fileOverride = context.ParseResult.GetValueForOption(OverrideOpt);
        // f2d d2d
        if (!target.Attributes.HasFlag(FileAttributes.Directory))
        {
            // file to directory,无需处理目录名
            var toFile = Path.Combine(output.FullName, target.Name.ReplaceAsSpan(replacers));
            await WriteToFileAsync(target, replacers, skipContent, fileOverride, toFile);
        }
        else
        {
            // directory to directory
            // d2d 总是加一级 outputRoot=output+target.Name
            var outputRoot = Path.Combine(output.FullName, target.Name.ReplaceAsSpan(replacers));
            var outputDirectory = new DirectoryInfo(outputRoot);
            if (outputDirectory.FullName == target.FullName)
            {
                throw new ArgumentNullException(nameof(output), VzLocales.L(VzLocales.Keys.RenameOutputIsTargetPrompt));
            }

            var fromFiles = new List<FileInfo>();
            CollecteFromFiles((DirectoryInfo)target, outputDirectory, fromFiles);

            // 重命名目录、重命名文件名、替换文件内容
            foreach (var fromFile in fromFiles)
            {
                // keep sub paths
                var toFile = Path.Join( // Path.Combine will reset root when it meet a '/'
                                ".",
                                Path.GetRelativePath(".", outputRoot),
                                Path.GetFullPath(fromFile.FullName)
                                    .Replace(
                                        Path.GetFullPath(target.FullName).EnsureEndsWithDirectorySeparatorChar(),
                                        string.Empty)
                                    .ReplaceAsSpan(replacers));
                await WriteToFileAsync(fromFile, replacers, skipContent, fileOverride, toFile);
            }
        }

        async Task WriteToFileAsync(FileSystemInfo target, Dictionary<string, string>? replacers, bool skipContent, bool? fileOverride, string toFile)
        {
            await CopyFileWrapAsync(toFile, fileOverride, async () =>
            {
                if (skipContent)
                {
                    File.Copy(target.FullName, toFile, true);
                }
                else
                {
                    var content = await ((FileInfo)target).OpenText().ReadToEndAsync();
                    content = content.ReplaceAsSpan(replacers);
                    await File.WriteAllTextAsync(toFile, content);
                }
            });
        }
    }

    private void CollecteFromFiles(DirectoryInfo target, DirectoryInfo outputRoot, List<FileInfo> fromFiles)
    {
        fromFiles.AddRange(target.GetFiles());

        foreach (var item in target.GetDirectories())
        {
            if (item.FullName == outputRoot.FullName)
            {
                // 处理过程排除 output 目录，（可能出现output目录在target目录下，比如 target 为 当前目录，-o 未指定，默认也为当前目录）
                continue;
            }

            CollecteFromFiles(item, outputRoot, fromFiles);
        }
    }

    private async Task CopyFileWrapAsync(string toFile, bool? fileOverride, Func<Task> copyFactory)
    {
        var to = new FileInfo(toFile);
        if (to.Exists)
        {
            bool doIt;
            if (!fileOverride.HasValue)
            {
                doIt = Prompt.Confirm(
                  VzLocales.L(VzLocales.Keys.GOutputFileExistsOverridePrompt, to.FullName),
                  false);
            }
            else
            {
                doIt = fileOverride.Value;
            }

            if (doIt)
            {
                await copyFactory();
            }
        }
        else
        {
            if (!to.Directory.Exists)
            {
                to.Directory.Create();
            }

            await copyFactory();
        }
    }
}