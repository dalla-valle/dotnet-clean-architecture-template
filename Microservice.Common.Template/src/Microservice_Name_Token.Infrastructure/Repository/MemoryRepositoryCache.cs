namespace Microservice_Name_Token.Infrastructure.Repository;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.Extensions.Configuration;
using ZiggyCreatures.Caching.Fusion;
using System.Linq;

public class MemoryRepositoryCache<T> : IRepository<T>
{
    private readonly IFusionCache memoryCache;
    private readonly IConfiguration configuration;

    public MemoryRepositoryCache(IFusionCache memoryCache, IConfiguration configuration)
    {
        this.memoryCache = memoryCache;
        this.configuration = configuration;
    }

    public async Task Drop(string id)
    {
        await this.memoryCache.RemoveAsync($"{typeof(T).Name}:{id}");
    }

    public async Task<ErrorOr<T>> Get(string id)
    {
        var cached = await this.memoryCache.TryGetAsync<T>($"{typeof(T).Name}:{id}");
        if (cached.HasValue)
        {
            return cached.Value;
        }

        return Error.NotFound($"No cache entry for key: {typeof(T).Name}:{id}");
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        var cached = await this.memoryCache.TryGetAsync<IEnumerable<T>>($"{typeof(T).Name}:GetAll");
        if (cached.HasValue)
        {
            return cached.Value;
        }

        return Enumerable.Empty<T>();
    }

    public async Task UpsertAll(IEnumerable<T> list)
    {
        var existing = (await this.GetAll()).ToList();
        var newList = list.ToList();

        // Get existing IDs
        var existingIds = new HashSet<object>(existing
            .Where(e => e != null)
            .Select(e => (object)((dynamic)e).id));

        // Add only entities with unique IDs
        var merged = existing
            .Concat(newList.Where(e => e != null && !existingIds.Contains((object)((dynamic)e).id)))
            .ToList();

        var cached = merged;
        await this.memoryCache.SetAsync($"{typeof(T).Name}:GetAll", cached);

        // Cache each entity individually
        foreach (var entity in list.ToList())
        {
            if (entity is null)
            {
                continue;
            }

            var id = ((dynamic)entity).id;
            if (id is null)
            {
                continue;
            }

            await this.memoryCache.SetAsync($"{typeof(T).Name}:{id}", entity);
        }
    }

    public async Task<IEnumerable<T>> GetByQuery(string query)
    {
        var cached = await this.memoryCache.TryGetAsync<IEnumerable<T>>($"{typeof(T).Name}:{query}");
        if (cached.HasValue)
        {
            return cached.Value;
        }

        throw new Exception($"No cache entry for key: {typeof(T).Name}:{query}");
    }
}