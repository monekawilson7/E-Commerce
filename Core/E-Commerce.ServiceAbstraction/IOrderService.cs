using E_Commerce.Shared.DataTransferObjects.UserOrder;

namespace E_Commerce.ServiceAbstraction;
public interface IOrderService
{
    Task<Result<OrderResponse>> CreateAsync (OrderRequest request, string email, CancellationToken cancellationToken);
    Task<Result<OrderResponse>> GetOrderAsync (string email,Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<OrderResponse>> GetAllAsync (string email, CancellationToken cancellationToken);
    Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync (CancellationToken cancellationToken);



}
