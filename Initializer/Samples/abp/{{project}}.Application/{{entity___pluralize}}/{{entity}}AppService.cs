using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace {{project}}.{{entity|string.pluralize}};

[Authorize]
[RemoteService(IsEnabled = false)]
public class {{entity}}AppService: ApplicationService, I{{entity}}AppService
{
    private readonly IRepository<{{entity}}, Guid> _{{entity}}Repository;
    public {{entity}}AppService(
        IRepository<{{entity}}, Guid> {{entity}}Repository
    )
    {
        _{{entity}}Repository = {{entity}}Repository;
    }

    public async Task CreateAsync(Create{{entity}}Dto input)
    {
        // TODO: 
        throw new NotImplementedException();
    }
    public async Task UpdateAsync(Guid id, {{entity}}Dto input)
    {
        // TODO: 
        throw new NotImplementedException();
    }
    public async Task DeleteAsync(Guid id)
    {
        // TODO: 
        throw new NotImplementedException();
    }
    public async Task<{{entity}}Dto> GetAsync(Guid? id)
    {
        // TODO: 
        throw new NotImplementedException();
    }

    public async Task<PagedResultDto<{{entity}}Dto>> GetListAsync(PagedResultRequestDto input)
    {
        // TODO: 
        throw new NotImplementedException();
    }

}