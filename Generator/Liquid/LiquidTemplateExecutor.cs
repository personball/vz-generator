using System.CommandLine.Invocation;
using System.Text.Json;

using Scriban;
using Scriban.Runtime;

using Sharprompt;

using vz_generator.Commands;
using vz_generator.Commands.Settings;
using vz_generator.Generator.Liquid.Scriban;
using vz_generator.Localization;

namespace vz_generator.Generator.Liquid;

public class LiquidTemplateExecutor
{
    private readonly GeneratorSetting _setting;
    private readonly InvocationContext _context;
    public LiquidTemplateExecutor(GeneratorSetting setting, InvocationContext context)
    {
        _setting = setting;
        _context = context;
    }

    public async Task ExecuteAsync()
    {
        // load naming functions
        var vzFuncs = new VzStringUtils();
        var tplContext = new TemplateContext();
        tplContext.PushGlobal(vzFuncs);

        if (_setting.Variables.Any())
        {
            var variableObj = new ScriptObject();
            foreach (var item in _setting.Variables)
            {
                if (item.Type == TemplateVariableType.String)
                {
                    // load string variables
                    variableObj.Add(item.Name, item.DefaultValue);
                }

                if (item.Type == TemplateVariableType.JsonFile)
                {
                    // load json file as dynamic object
                    var file = new FileInfo(item.DefaultValue);// TODO: verify 绝对路径？相对路径？
                    if (!file.Exists)
                    {
                        throw new ArgumentNullException("--var-json-file", $"{item.DefaultValue} Not Found!");
                    }

                    var text = await file.OpenText().ReadToEndAsync();
                    using var json = JsonDocument.Parse(text);
                    variableObj.Add(item.Name, ConvertFromJson(json.RootElement));
                }
            }

            tplContext.PushGlobal(variableObj);
        }

        // enumerate templates and paths
        // config one tpl file to output file: Liquid <output path> and <output file name>
        // config one tpl file to output directory: Liquid <output path> and <tpl file name> 
        // config tpl directory (multi tpls) to output directory: Liquid <output path> and <tpl sub path> and <tpl file name>

        var tplFiles = new List<FileInfo>();
        var tplRoot = _setting.TemplatePath;
        if (!File.Exists(tplRoot) && !Directory.Exists(tplRoot))
        {
            throw new ArgumentNullException(nameof(_setting.TemplatePath), $"{tplRoot} Not Exists!");
        }

        var tplRootFileAttrs = File.GetAttributes(tplRoot);

        if (tplRootFileAttrs.HasFlag(FileAttributes.Directory))
        {
            // directory
            CollectTemplateFiles(tplRoot, tplFiles);
        }
        else
        {
            // file
            tplFiles.Add(new FileInfo(tplRoot));
            // reset tplRoot as directory
            tplRoot = Path.GetDirectoryName(tplRoot);
        }

        if (!tplFiles.Any())
        {
            _context.Console.Out.Write(
                VzLocales.L(VzLocales.Keys.GTemplateFileNotFoundPrompt));
            return;
        }

        var outputRoot = _setting.Output.Replace("___", "|").RenderContent(tplContext);//exists or not, directory or file
        var outputIsFile = false;

        if (!outputRoot.EndsWith(Path.DirectorySeparatorChar))
        {
            outputIsFile = true;
        }

        if (tplFiles.Count > 1 && outputIsFile)
        {
            throw new ArgumentException(
                nameof(_setting.Output),
                VzLocales.L(
                    VzLocales.Keys.GMultiTplOutputToSingleFileError,
                    "" + tplFiles.Count,
                    outputRoot,
                    "" + Path.DirectorySeparatorChar));
        }

        foreach (var item in tplFiles)
        {
            // read
            var tplContent = await item.OpenText().ReadToEndAsync();

            // render
            var outputContent = tplContent.RenderContent(tplContext);

            // replace tplRoot with outputRoot
            string outputFilePath = CalculateOutputFilePath(tplRoot, item.FullName, outputRoot, outputIsFile);
            outputFilePath = outputFilePath.Replace("___", "|").RenderContent(tplContext);

            // output directory
            var outputDirectory = Path.GetDirectoryName(outputFilePath);
            if (!string.IsNullOrWhiteSpace(outputDirectory) && !Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            // output file name
            var fileOverride = _context.ParseResult.GetValueForOption(GenerateCommand.OverrideOpt);
            if (File.Exists(outputFilePath))
            {
                // if -w always override output
                var watched = _context.ParseResult.GetValueForOption(GenerateCommand.WatchOpt);
                if (watched)
                {
                    fileOverride = true;
                }

                // 存在，是否覆盖？
                var doIt = false;
                if (!fileOverride.HasValue)
                {
                    doIt = Prompt.Confirm(
                        VzLocales.L(VzLocales.Keys.GOutputFileExistsOverridePrompt, outputFilePath),
                        false);
                }
                else
                {
                    doIt = fileOverride.Value;
                }

                if (doIt)
                {
                    await File.WriteAllTextAsync(outputFilePath, outputContent);
                }
            }
            else
            {
                // 不存在，则创建
                await File.WriteAllTextAsync(outputFilePath, outputContent);
            }
        }

        // TODO: one tpls to multi ouput (support scriban function to output files)
    }

    private string CalculateOutputFilePath(string tplRoot, string tplFullName, string outputRoot, bool outputIsFile)
    {
        if (outputIsFile)
        {
            return outputRoot;
        }
        // keep sub paths
        return Path.Join( // Path.Combine will reset root when it meet a '/'
            ".",
            Path.GetRelativePath(".", outputRoot),
            Path.GetFullPath(tplFullName)
                .Replace(
                    Path.GetFullPath(tplRoot)
                        .EnsureEndsWithDirectorySeparatorChar(), string.Empty));
    }

    private void CollectTemplateFiles(string tplRoot, List<FileInfo> tplFiles)
    {
        var files = Directory.GetFiles(tplRoot);
        foreach (var file in files)
        {
            tplFiles.Add(new FileInfo(file));
        }

        foreach (var item in Directory.GetDirectories(tplRoot))
        {
            CollectTemplateFiles(item, tplFiles);
        }
    }

    // 转换 Json 对象为 IScriptObject
    // https://github.com/lunet-io/lunet/blob/54ed2989f92883d925f89b04f36366c229896fba/src/Lunet.Json/JsonUtil.cs#L62-L119
    private static object ConvertFromJson(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                var obj = new ScriptObject();
                foreach (var prop in element.EnumerateObject())
                {
                    obj[prop.Name] = ConvertFromJson(prop.Value);
                }

                return obj;
            case JsonValueKind.Array:
                var array = new ScriptArray();
                foreach (var nestedElement in element.EnumerateArray())
                {
                    array.Add(ConvertFromJson(nestedElement));
                }
                return array;
            case JsonValueKind.String:
                return element.GetString();
            case JsonValueKind.Number:
                if (element.TryGetInt32(out var intValue))
                {
                    return intValue;
                }
                else if (element.TryGetInt64(out var longValue))
                {
                    return longValue;
                }
                else if (element.TryGetUInt32(out var uintValue))
                {
                    return uintValue;
                }
                else if (element.TryGetUInt64(out var ulongValue))
                {
                    return ulongValue;
                }
                else if (element.TryGetDecimal(out var decimalValue))
                {
                    return decimalValue;
                }
                else if (element.TryGetDouble(out var doubleValue))
                {
                    return doubleValue;
                }
                else
                {
                    throw new InvalidOperationException($"Unable to convert number {element}");
                }
            case JsonValueKind.True:
                return BoolTrue;
            case JsonValueKind.False:
                return BoolFalse;
            default:
                return null;
        }
    }
    private static readonly object BoolTrue = true;
    private static readonly object BoolFalse = false;
}