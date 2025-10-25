using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Basket;
using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.Basket;

namespace E_Commerce.Service.Services;
internal class BasketService (IBasketRepository basketRepository, IMapper mapper)
    : IBasketService
{
    public async Task<CustomerBasketDTO> CreateOrUpdateAsync(CustomerBasketDTO basketDto)
    {
        var basket = mapper.Map<CustomerBasket>(basketDto);
        var updatedBasket = await basketRepository.CreateOrUpdateAsync(basket);
        return mapper.Map<CustomerBasketDTO>(updatedBasket);
    }

    public Task DeleteAsync(string id)
    {
        return basketRepository.DeleteAsync(id);
    }

    public async Task<CustomerBasketDTO> GetByIdAsync(string id)
    {
        var basket = await basketRepository.GetAsync(id);
        return mapper.Map<CustomerBasketDTO>(basket);

    }
}
