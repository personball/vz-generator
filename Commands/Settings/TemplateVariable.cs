using Json.Schema.Generation;

namespace vz_generator.Commands.Settings;

public class TemplateVariable
{
    /// <summary>
    /// 变量名，供模板中引用
    /// </summary>
    /// <value></value>
    [Required]
    // [Description("Variable Name")]
    public string Name { get; set; }

    /// <summary>
    /// String 类型直接输入；JsonFile 需要指定文件路径
    /// </summary>
    /// <value></value>
    // [Description("Variable type, default is String")]
    public TemplateVariableType Type { get; set; } = TemplateVariableType.String;

    /// <summary>
    /// FileInfo? 
    /// </summary>
    /// <value></value>
    // [Description("Set filePath when type equals JsonFile")]
    public string? FilePath { get; set; }

}
