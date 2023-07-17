using Json.Schema.Generation;

namespace vz_generator.Commands.Settings;

public class TemplateVariable
{
    /// <summary>
    /// 变量名，供模板中引用
    /// </summary>
    /// <value></value>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// String 类型直接输入；JsonFile 需要指定文件路径
    /// </summary>
    /// <value></value>
    public TemplateVariableType Type { get; set; } = TemplateVariableType.String;

    /// <summary>
    /// 默认值
    /// </summary>
    /// <value></value>
    public string? DefaultValue { get; set; }
}
