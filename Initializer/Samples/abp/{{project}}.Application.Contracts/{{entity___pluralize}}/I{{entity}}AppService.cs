using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using {{project}}.{{entity|pluralize}}.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace {{project}}.{{entity|pluralize}};
public interface I{{entity}}AppService: IApplicationService
{
    Task CreateAsync(Create{{entity}}Dto input);
    
    Task UpdateAsync(Guid id, Update{{entity}}Dto input);
    
    Task DeleteAsync(Guid id);
    
    Task<{{entity}}Dto> GetAsync(Guid id);
    
    Task<PagedResultDto<{{entity}}Dto>> GetListAsync(PagedResultRequestDto input);
}