using E_Commerce.Shared.DataTransferObjects.UserOrder;

namespace E_Commerce.ServiceAbstraction;
public interface IOrderService
{
    Task<Result<OrderResponse>> CreateAsync (OrderRequest request, string email);
}
