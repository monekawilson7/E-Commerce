using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.OrderEntities;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Service.Specifications;
using E_Commerce.Shared.DataTransferObjects.UserOrder;

namespace E_Commerce.Service.Services;
internal class OrderService(IUnitOfWork unitOfWork,
    IMapper mapper,
    IBasketRepository basketRepository)
    : IOrderService
{
    public async Task<Result<OrderResponse>> CreateAsync(OrderRequest request, string email)
    {
        var basket = await basketRepository.GetAsync(request.basketId);
        if(basket == null)
            return Error.NotFound("Basket not found", 
                $"basket with Id {request.basketId} was not found");
        var method = await unitOfWork.GetRepository<DeliveryMethod, int>()
            .GetByIdASync(request.DeliveryMethodId);
        if(method == null)
            return Error.NotFound("Delivery method not found", 
                $"Delivery method with Id {request.DeliveryMethodId} was not found");

        var productRepo = unitOfWork.GetRepository<Product, int>();
        var ids = basket.Items.Select(i => i.Id).ToList();
        var products = (await productRepo.GetAllAysnc(new GetproductsByIdsSpecification(ids)))
            .ToDictionary(p=> p.Id);
        var orderItems = new List<OrderItem>();
        var validationErrors = new List<Error>();

        foreach (var item in basket.Items)
        {
            if (!products.TryGetValue(item.Id, out Product? product ))
            { 
                validationErrors.Add(Error.Validation(""));
                continue;
            }
            var orderItem = new OrderItem
            {
                Price = product.Price,
                Quantity = item.Quantity,
                product = new ProductInOrderItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    PictureUrl = product.PictureUrl
                }
            };
            orderItems.Add(orderItem);

        }

        if (validationErrors.Any())
            return validationErrors; 

        var subtotal = orderItems.Sum(i => i.Price * i.Quantity);
        var order = new Order{ 
        DeliveryMethod = method,
        UserEmail = email,
        Items = orderItems,
        Subtotal = subtotal,
        Address = mapper.Map<OrderAddress>(request.address)
        };
        var orderRepo = unitOfWork.GetRepository<Order, Guid>();
        orderRepo.Add(order);
        await unitOfWork.SaveChangesAysnc();
        return mapper.Map<OrderResponse>(order);
    }

    public async Task<Result<OrderResponse>> GetByIdAsync(Guid id)
    {
        var order = await unitOfWork.GetRepository<Order, Guid>()
            .GetASync(new OrderByIdSpecification(id));
        if(order == null)
            return Error.NotFound("Order not found", 
                $"Order with Id {id} was not found");
        return mapper.Map<OrderResponse>(order);
    }

    public async Task<IEnumerable<OrderResponse>> GetByUserEmailAsync(string email)
    {
        var orders = await unitOfWork.GetRepository<Order, Guid>()
            .GetAllAysnc(new OrderByEmailSpecifications(email));

        return mapper.Map<IEnumerable<OrderResponse>>(orders);
    }

    public async Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync()
    {
        var methods = await unitOfWork.GetRepository<DeliveryMethod, int>()
            .GetAllAysnc();
        return mapper.Map<IEnumerable<DeliveryMethodResponse>>(methods);
    }
}
