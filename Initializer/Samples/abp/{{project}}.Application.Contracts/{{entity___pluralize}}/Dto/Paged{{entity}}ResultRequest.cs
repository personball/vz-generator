using Volo.Abp.Application.Dtos;

namespace {{project}}.{{entity|pluralize}}.Dto;

public record Paged{{entity}}ResultRequest : PagedResultRequestDto
{
    /// <summary>
    /// 关键词
    /// </summary>
    public string? Filter { get; set; }
}