using System.ComponentModel.DataAnnotations;

namespace {{project}}.{{entity|pluralize}}.Dto;

public class Create{{entity}}Dto
{
   /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(ConsumableConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(ConsumableConsts.DescriptionMaxLength)]
    public string Description { get; set; } = string.Empty;

    // TODO: add properties
};
