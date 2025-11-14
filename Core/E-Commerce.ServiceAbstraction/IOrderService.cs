using E_Commerce.Shared.DataTransferObjects.UserOrder;

namespace E_Commerce.ServiceAbstraction;
public interface IOrderService
{
    Task<Result<OrderResponse>> CreateAsync (OrderRequest request, string email);
    Task<Result<OrderResponse>> GetByIdAsync (Guid id);
    Task<IEnumerable<OrderResponse>> GetByUserEmailAsync (string email);
    Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync ();



}
