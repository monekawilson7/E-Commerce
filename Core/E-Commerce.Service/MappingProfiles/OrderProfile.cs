using AutoMapper;
using E_Commerce.Domain.Entities.OrderEntities;
using E_Commerce.Shared.DataTransferObjects.UserOrder;
using E_Commerce.Shared.DataTransferObjects.Users;

namespace E_Commerce.Service.MappingProfiles;
internal class OrderProfile 
    : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponse>()
            .ForMember(d=> d.DeliveryMethod,
            o=> o.MapFrom(s=> s.DeliveryMethod.ShortName))
            .ForMember(d => d.DeliveryMethod,
            o => o.MapFrom(s => s.DeliveryMethod.Price))
            .ForMember(d => d.Total,
            o => o.MapFrom(s => s.DeliveryMethod.Price + s.Subtotal));
        CreateMap<OrderAddress,AddressDTO>()
            .ReverseMap();
        CreateMap<OrderItem, OrderItemDTO>()
            .ForMember(d => d.ProductId,
            o => o.MapFrom(s => s.product.ProductId))
            .ForMember(d => d.Name,
            o => o.MapFrom(s => s.product.Name));
        CreateMap<DeliveryMethod, DeliveryMethodResponse>()
            .ForMember(d=> d.Cost,
            o=>o.MapFrom(s=>s.Price));

    }
}
