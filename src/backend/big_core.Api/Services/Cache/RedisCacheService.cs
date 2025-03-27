namespace big_core.Api.Services.Cache;

using StackExchange.Redis;
using System.Text.Json;

public class RedisCacheService(IConnectionMultiplexer redis) : ICacheService
{
    private readonly IDatabase _cache = redis.GetDatabase();

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        var jsonData = JsonSerializer.Serialize(value);
        await _cache.StringSetAsync(key, jsonData, expiration);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var jsonData = await _cache.StringGetAsync(key);
        return jsonData.HasValue ? JsonSerializer.Deserialize<T>(jsonData!) : default;
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.KeyDeleteAsync(key);
    }
}
