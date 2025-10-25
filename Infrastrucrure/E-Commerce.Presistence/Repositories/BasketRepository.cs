using E_Commerce.Domain.Entities.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Presistence.Repositories;
internal class BasketRepository(IConnectionMultiplexer multiplexer) 
    : IBasketRepository
{
    private readonly IDatabase _database = multiplexer.GetDatabase();
    public async Task<CustomerBasket> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan? TTL = null)
    {
        var jason = JsonSerializer.Serialize(basket);
        await _database.StringSetAsync(basket.ID, jason, TTL?? TimeSpan.FromDays(7));
        return (await GetAsync(basket.ID))!;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _database.KeyDeleteAsync(id);
    }

    public async Task<CustomerBasket?> GetAsync(string id)
    {
        var jason = await _database.StringGetAsync(id);
        if(jason.IsNullOrEmpty)
            return null;
        return JsonSerializer.Deserialize<CustomerBasket>(jason!);

    }
}
