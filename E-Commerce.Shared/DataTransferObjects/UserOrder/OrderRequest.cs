using E_Commerce.Shared.DataTransferObjects.Users;

namespace E_Commerce.Shared.DataTransferObjects.UserOrder;
public record OrderRequest(AddressDTO address, string basketId, int DeliveryMethodId);
