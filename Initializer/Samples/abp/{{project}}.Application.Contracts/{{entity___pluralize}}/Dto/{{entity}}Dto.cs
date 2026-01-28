using System;
using Volo.Abp.Application.Dtos;

namespace {{project}}.{{entity|pluralize}}.Dto;

public class {{entity}}Dto: EntityDto<Guid>
{
    public string Name { get; set; } = null!;
}
