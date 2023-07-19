using System.CommandLine.Invocation;
using System.Dynamic;
using System.Text.Json;
using Scriban;
using Scriban.Runtime;
using Sharprompt;
using vz_generator.Commands;
using vz_generator.Commands.Settings;
using vz_generator.Generator.Liquid.Scriban;

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
                    dynamic json = JsonSerializer.Deserialize<ExpandoObject>(text);

                    variableObj.Add(item.Name, json);
                }
            }

            tplContext.PushGlobal(variableObj);
        }

        // TODO: enumerate templates and paths
        // config one tpl file to output file: Liquid <output path> and <output file name>
        // config one tpl file to output directory: Liquid <output path> and <tpl file name> 
        // config tpl directory (multi tpls) to output directory: Liquid <output path> and <tpl sub path> and <tpl file name>

        var tplFiles = new List<FileInfo>();
        var tplRoot = _setting.TemplatePath;
        if (!File.Exists(tplRoot) && !Directory.Exists(tplRoot))
        {
            throw new ArgumentNullException(nameof(_setting.TemplatePath), $"{tplRoot} Not Exists!");
        }

        var fileAttrs = File.GetAttributes(tplRoot);

        if (fileAttrs.HasFlag(FileAttributes.Directory))
        {
            // directory
            CollectTemplateFiles(tplRoot, tplFiles);
        }
        else
        {
            // file
            tplFiles.Add(new FileInfo(tplRoot));
        }

        if (!tplFiles.Any())
        {
            _context.Console.Out.Write($"There is no template file, nothing happened.\n");
            return;
        }

        var outputRoot = _setting.Output.Replace("___", "|").RenderContent(tplContext);//exists or not, directory or file
        var outputIsDirectory = false;
        var outputIsFile = false;

        if (outputRoot.EndsWith(Path.DirectorySeparatorChar))
        {
            outputIsDirectory = true;
        }
        else
        {
            outputIsFile = true;
        }

        if (tplFiles.Count > 1 && outputIsFile)
        {
            throw new ArgumentException(nameof(_setting.Output),
                $"There are {tplFiles.Count} Template Files, but {outputRoot} is not end with '{Path.DirectorySeparatorChar}'.");
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
                // 存在，是否覆盖？
                var doIt = false;
                if (!fileOverride.HasValue)
                {
                    doIt = Prompt.Confirm($"{outputFilePath} is already exists, override it?", false);
                }
                else
                {
                    doIt = fileOverride.Value;
                    // Console.WriteLine($"{nameof(fileOverride)}:{fileOverride.Value}");
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
            Path.GetFullPath(tplFullName).Replace(Path.GetFullPath(tplRoot), string.Empty));
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
}
/*
"""
        author: {{json.name|string.upcase}}
        NameIt == {{'nameIt'|pascal_case}}
        nameIt == {{'NameIt'|camel_case}}
        name-it == {{'NameIt'|kebab_case}}
        name_it == {{'NameIt'|snake_case}}
        people == {{'person'|pluralize}}
        person == {{'people'|singularize}}
        """
*/