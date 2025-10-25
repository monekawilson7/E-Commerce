using E_Commerce.ServiceAbstraction;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Presistence.Services;
internal class CashService (IConnectionMultiplexer multiplexer)
    : ICashService
{
    private readonly IDatabase _database = multiplexer.GetDatabase();
    public async Task<string?> GetAsync(string key)
    {
        return await _database.StringGetAsync(key);
    }

    public async Task SetAsync(string key, object value, TimeSpan TTL)
    {
        var option = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var jason = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, jason, TTL);
    }
}

