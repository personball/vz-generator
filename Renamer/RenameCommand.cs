using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.Text;
using System.Text.Json;

using GitignoreParserNet;

using Sharprompt;

using ShellProgressBar;

using vz_generator.Localization;

namespace vz_generator.Renamer;

public sealed class RenameCommand : Command
{
    public RenameCommand()
        : base(VzConsts.RenameCmd.Name, VzLocales.L(VzLocales.Keys.RenameCommandDesc))
    {
        AddArgument(RenameTargetArg);

        foreach (var opt in Opts())
        {
            AddOption(opt);
        }
    }

    private static readonly Argument<FileSystemInfo> RenameTargetArg = new Argument<FileSystemInfo>(
        name: "target",
        description: VzLocales.L(VzLocales.Keys.RTargetArgDesc)
    );

    public static IEnumerable<Option> Opts()
    {
        yield return SkipContentOpt;
        yield return ReplacePairsOpt;
        yield return OutputOpt;
        yield return OverrideOpt;
        yield return IncludeOpt;
        yield return IncludeExtOpt;
        yield return ExcludeOpt;
        yield return ExcludeExtOpt;
        yield return ExcludePathPatternOpt;
        yield return ExcludeByGitignoreOpt;
        yield return AllFilesOpt;
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
        aliases: ["-o", "--output"],
        description: VzLocales.L(VzLocales.Keys.ROptOutputOptDesc),
        getDefaultValue: () => new DirectoryInfo(".")
    );

    private static readonly Option<bool?> OverrideOpt = new(
        name: "--override",
        description: VzLocales.L(VzLocales.Keys.ROptOverrideOptDesc),
        getDefaultValue: () => (bool?)null
    );

    private static readonly Option<bool> AllFilesOpt = new(
        name: "--all",
        description: VzLocales.L(VzLocales.Keys.ROptAllFilesOptDesc),
        getDefaultValue: () => false
    );

    private static readonly Option<List<string>> IncludeOpt = new(
        name: "--include",
        description: VzLocales.L(VzLocales.Keys.ROptIncludeOptDesc)
    );

    private static readonly Option<List<string>> ExcludeOpt = new(
       name: "--exclude",
       description: VzLocales.L(VzLocales.Keys.ROptExcludeOptDesc)
   );

    private static readonly Option<List<string>> IncludeExtOpt = new(
        name: "--include-ext",
        description: VzLocales.L(VzLocales.Keys.ROptIncludeExtOptDesc),
        parseArgument: result => result.Tokens.Select(t => t.Value.StartsWith('.') ? t.Value : ('.' + t.Value)).ToList()
    );

    private static readonly Option<List<string>> ExcludeExtOpt = new(
        name: "--exclude-ext",
        description: VzLocales.L(VzLocales.Keys.ROptExcludeExtOptDesc),
        parseArgument: result => result.Tokens.Select(t => t.Value.StartsWith('.') ? t.Value : ('.' + t.Value)).ToList()
    );

    private static readonly Option<List<string>> ExcludePathPatternOpt = new(
        aliases: ["--epp", "--exclude-path-pattern"],
        description: VzLocales.L(VzLocales.Keys.ROptExcludePathPatternDesc)
    );

    private static readonly Option<bool> ExcludeByGitignoreOpt = new(
        aliases: ["--gitignore", "--exclude-by-gitignore"],
        description: VzLocales.L(VzLocales.Keys.ROptExcludeByGitignoreDesc),
        getDefaultValue: () => false
    );

    // TODO: --no-copy? 需要文件系统事务性操作支持。
    public async Task RenameAsync(InvocationContext context)
    {
        try
        {
            await RenameInternalAsync(context);
        }
        catch (Exception ex)
        {
#if DEBUG
            Console.WriteLine($"Rename Fail:{ex.Message},{Environment.NewLine}");
#else
            context.Console.Error.Write(
                    VzLocales.L(
                        VzLocales.Keys.RenameFailedErrorResult, ex.Message, Environment.NewLine, ex.StackTrace ?? ""));
            context.ExitCode = 2;
#endif
        }
    }

    private async Task RenameInternalAsync(InvocationContext context)
    {
        var target = context.ParseResult.GetValueForArgument(RenameTargetArg);
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
            var toFile = Path.Combine(output!.FullName, target.Name.ReplaceAsSpan(replacers));
            await WriteToFileAsync(target, replacers, skipContent, fileOverride, toFile);
        }
        else
        {
            // directory to directory
            // d2d 总是加一级 outputRoot=output+target.Name
            var outputRoot = Path.Combine(output!.FullName, target.Name.ReplaceAsSpan(replacers));
            var outputDirectory = new DirectoryInfo(outputRoot);
            if (outputDirectory.FullName == target.FullName)
            {
                throw new ArgumentNullException(nameof(output), VzLocales.L(VzLocales.Keys.RenameOutputIsTargetPrompt));
            }

            var fromFiles = new List<FileInfo>();
            CollecteFromFiles((DirectoryInfo)target, outputDirectory, fromFiles, files =>
            {
                var all = context.ParseResult.GetValueForOption(AllFilesOpt);
                if (all)
                {
                    // TODO: BreakChange, deprecate include include-ext exclude exclude-ext, remove those default values, and set default --gitignore to true
                    // 启用 --gitignore 选项的情况下，默认应该令 --all 为 false，令 --gitignore 为 true
                    // 明确 --all 为true时，则 --gitignore 应失效
                    // 现在临时兼容处理，原先选项生效之后，继续叠加 gitignore 规则
                    return files;
                }

                var included = context.ParseResult.GetValueForOption(IncludeOpt) ?? new List<string>();
                included.AddRange(DefaultIncluded);
                var includedExts = context.ParseResult.GetValueForOption(IncludeExtOpt) ?? new List<string>();
                includedExts.AddRange(DefaultIncludedExts);
                var excluded = context.ParseResult.GetValueForOption(ExcludeOpt) ?? new List<string>();
                var excludedExts = context.ParseResult.GetValueForOption(ExcludeExtOpt) ?? new List<string>();

                var includedFiles = files
                            .Where(f =>
                                included.Any(e => string.Equals(e, f.Name, StringComparison.OrdinalIgnoreCase))
                                || includedExts.Any(e => string.Equals(e, f.Extension, StringComparison.OrdinalIgnoreCase)))
                            .Where(f =>
                                !excluded.Any(e => string.Equals(e, f.Name, StringComparison.OrdinalIgnoreCase))
                                && !excludedExts.Any(e => string.Equals(e, f.Extension, StringComparison.OrdinalIgnoreCase)));

                return includedFiles;
            });

            var exts = fromFiles.Select(f => f.Extension).Distinct().ToList();

#if DEBUG
            context.Console.Out.WriteLine(JsonSerializer.Serialize(exts) + $"{Environment.NewLine}Above Exts Processed!");
#endif

            var epp = context.ParseResult.GetValueForOption(ExcludePathPatternOpt);
            var gitignore = context.ParseResult.GetValueForOption(ExcludeByGitignoreOpt);

            if ((epp != null && epp.Any()) || gitignore)
            {
                var rules = string.Empty;

                if (gitignore)
                {
                    var rulePath = Path.Combine(target.FullName, ".gitignore");
                    if (File.Exists(rulePath))
                    {
                        rules += File.ReadAllText(rulePath, Encoding.UTF8);
                    }
                    else
                    {
#if DEBUG
                        context.Console.WriteLine($"{rulePath} not Exists, .gitignore NOT loaded.");
#endif
                    }

                    rules += $"{Environment.NewLine}.git/";
                }

                if (epp != null && epp.Any())
                {
                    foreach (var rule in epp)
                    {
                        rules += $"{Environment.NewLine}{rule}";
                    }
                }

                var gitignoreParser = new GitignoreParser(rules);

                fromFiles = fromFiles.Where(f => gitignoreParser.Accepts(f.FullName)).ToList();
            }

            using var pbar = new ProgressBar(fromFiles.Count, "Working...");
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

                pbar.Tick($"{toFile} finished.");
            }
        }

        async Task WriteToFileAsync(FileSystemInfo target, Dictionary<string, string> replacers, bool skipContent, bool? fileOverride, string toFile)
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
    private static readonly string[] DefaultIncluded = new[]{
"dockerfile"
};
    private static readonly string[] DefaultIncludedExts = new[]{
".astro",
".coffee",
".config",
".cs",
".cshtml",
".csproj",
".DOCS",
".DotSettings",
".DS_Store",
".editorconfig",
".gitignore",
".html",
".js",
".json",
".jsx",
".Makefile",
".markdown",
".md",
".nuspec",
".props",
".ps1",
".sh",
".sln",
".svelte",
".ts",
".tsx",
".txt",
".vue",
".xaml",
".xml",
".yml",
};

    private void CollecteFromFiles(DirectoryInfo target, DirectoryInfo outputRoot, List<FileInfo> fromFiles, Func<IEnumerable<FileInfo>, IEnumerable<FileInfo>> fileFilter)
    {
        fromFiles.AddRange(fileFilter(target.GetFiles()));

        foreach (var item in target.GetDirectories())
        {
            if (item.FullName == outputRoot.FullName)
            {
                // 处理过程排除 output 目录，（可能出现output目录在target目录下，比如 target 为 当前目录，-o 未指定，默认也为当前目录）
                continue;
            }

            CollecteFromFiles(item, outputRoot, fromFiles, fileFilter);
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
            if (!to.Directory!.Exists)
            {
                to.Directory.Create();
            }

            await copyFactory();
        }
    }
}