using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Sm.Crm.Application.Common.Constants;
using Sm.Crm.Application.Common.Interfaces;
using Sm.Crm.Domain.Common;
using System.Text.Json;

namespace Sm.Crm.Infrastructure.Caching;

public class RedisCache : IAppCache
{
    private readonly IDistributedCache _redis;
    private readonly IConfiguration _configuration;

    public RedisCache(IDistributedCache redis, IConfiguration configuration)
    {
        _redis = redis;
        _configuration = configuration;
    }

    public async Task SetCache(string key, string value, int timeout = 1)
    {
        if (_configuration["App:IsRedisActive"] != "true") return;

        var options = new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromHours(timeout),
            AbsoluteExpiration = DateTime.Now.AddHours(timeout)
        };
        await _redis.SetStringAsync(key, value, options, default);
    }

    public async Task<string?> GetCache(string key)
    {
        if (_configuration["App:IsRedisActive"] != "true") return null;

        return await _redis.GetStringAsync(key, default);
    }

    public async Task RemoveCache(string key)
    {
        if (_configuration["App:IsRedisActive"] != "true") return;

        await _redis.RemoveAsync(key);
    }

    public async Task<T?> GetOrSetCache<T>(string key, Func<T> action, int timeout = 1)
    {
        if (_configuration["App:IsRedisActive"] != "true") return action();
        
        var cacheItems = await GetCache(key);
        if (cacheItems != null)
            return JsonSerializer.Deserialize<T>(cacheItems);

        var itemsDb = action();
        await SetCache(key, JsonSerializer.Serialize(itemsDb), timeout);
        return itemsDb;
    }
}