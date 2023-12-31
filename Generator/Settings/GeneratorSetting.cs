using Json.Schema.Generation;

namespace vz_generator.Commands.Settings;

public class GeneratorSetting
{
    /// <summary>
    /// Unique option name for follow settings 
    /// argument.0(可选，没有则输出select提示，由用户选择；若包含空格，输入时需要用引号)
    /// </summary>
    /// <value></value>
    [Required]
    [Description("Unique option name for CLI select.")]
    public string Option { get; set; } = string.Empty;

    /// <summary>
    /// Liquid or Razor
    /// opt(仅可声明一次，指定模板语法):
    ///     -s Razor
    ///     --syntax Razor
    /// </summary>
    /// <value></value>
    [Description("Liquid or Razor(not supported yet) according to your preferred template syntax.")]
    public TemplateSyntax TemplateSyntax { get; set; } = TemplateSyntax.Liquid;

    /// <summary>
    /// Path for single template file or multi templates folder
    /// opt(仅可声明一次，指定目录或单个文件):
    ///     -t=./xxx/
    ///     --template=./xxx/__store__.js
    /// </summary>
    /// <value></value>
    [Required]
    [Description("Path for single template file or multi templates folder.")]
    public string TemplatePath { get; set; } = string.Empty;

    /// <summary>
    /// 变量名、类型、文件路径
    /// opt (可以声明多次): 
    ///     --var-string a=b 
    ///     --var-json-file name=./xxx/filename.json 
    /// </summary>
    /// <value></value>
    [Description("Variables declarations, can be overrided by --var a=b or --var-json-file a=./xxx/1.json .")]
    public List<TemplateVariable> Variables { get; set; } = new List<TemplateVariable>();

    /// <summary>
    /// 输出
    /// 1. 如果未配置，默认输出到当前目录下的output中；
    /// 2. 如果 TemplatePath 为目录（不论目录内有多少个模板文件，没有模板文件则在加载时报错）
    ///     2.1 如果输出为文件（不以/结尾），则报错；
    ///     2.2 如果输出为一个目录（以/结尾），则输出到指定目录（保留TemplatePath指定目录的子目录结构），且允许以对应语法进行目录名的变量替换；
    /// 3. 如果 TemplatePath 为文件（单一文件）
    ///     3.1 如果输出为文件（不以/结尾），则文件名优先按 output 指定的进行转换，且允许以对应语法进行目录名和文件名的变量替换；
    ///     3.2 如果输出为一个目录（以/结尾），则输出到指定目录，且允许以对应语法进行目录名的变量替换，同时按模板文件的文件名进行转换输出，允许以对应的语法对文件名进行变量名替换；
    /// </summary>
    /// <value></value>
    [Description("Path for output. If output is a directory, make sure ends with / or \\ .")]
    public string Output { get; set; } = string.Empty;

    /// <summary>
    /// 加载自定义函数、指令
    /// </summary>
    /// <value></value>
    // [Description("Import custom functions or directives.")]
    // public string Imports { get; set; }
}