using E_Commerce.Shared.DataTransferObjects.Basket;

namespace E_Commerce.ServiceAbstraction;
public interface IBasketService
{
    Task<CustomerBasketDTO> CreateOrUpdateAsync (CustomerBasketDTO basketDto);
    Task<CustomerBasketDTO> GetByIdAsync (string id);
    Task DeleteAsync (string id);
}
