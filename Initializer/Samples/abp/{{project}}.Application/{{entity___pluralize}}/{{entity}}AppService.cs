using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using {{project}}.{{entity|pluralize}}.Dto;

namespace {{project}}.{{entity|pluralize}};

[Authorize]
[RemoteService(IsEnabled = false)]
public class {{entity}}AppService: {{project}}AppService, I{{entity}}AppService
{
    private readonly IRepository<{{entity}}, Guid> _{{entity|string.downcase}}Repo;
    public {{entity}}AppService(
        IRepository<{{entity}}, Guid> {{entity|string.downcase}}Repo
    )
    {
        _{{entity|string.downcase}}Repo = {{entity|string.downcase}}Repo;
    }

    public async Task CreateAsync(Create{{entity}}Dto input)
    {
        var item = ObjectMapper.Map<Create{{entity}}Dto, {{entity}}>(input);
        // TODO: 
        await _{{entity|string.downcase}}Repo.InsertAsync(item);
    }

    public async Task UpdateAsync(Guid id, Update{{entity}}Dto input)
    {
        var item = await _{{entity|string.downcase}}Repo.GetAsync(id);
        ObjectMapper.Map(input, item);
        // TODO: 
    }

    public async Task DeleteAsync(Guid id)
    {
        // TODO: 
        await _{{entity|string.downcase}}Repo.DeleteAsync(id);
    }
    public async Task<{{entity}}Dto> GetAsync(Guid id)
    {
        // TODO: 
        var item = await _{{entity|string.downcase}}Repo.GetAsync(id);
        return ObjectMapper.Map<{{entity}}, {{entity}}Dto>(item);
    }

    public async Task<PagedResultDto<{{entity}}Dto>> GetListAsync(Paged{{entity}}ResultRequest input)
    {
        // TODO: 
         var query = await _{{entity|string.downcase}}Repo.GetQueryableAsync();
        query = query.WhereIf(!input.Filter.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Filter!) || p.Description.Contains(input.Filter!));

        var count = await AsyncExecuter.CountAsync(query);
        var items = await AsyncExecuter.ToListAsync(query.OrderByDescending(p => p.CreationTime).PageBy(input));

        return new PagedResultDto<{{entity}}Dto>
        {
            TotalCount = count,
            Items = ObjectMapper.Map<List<{{entity}}>, List<{{entity}}Dto>>(items)
        };
    }

}