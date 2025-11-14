using E_Commerce.Domain.Entities.OrderEntities;

namespace E_Commerce.Service.Specifications;
internal class OrderByIdSpecification
    :BaseSpecification<Order>
{
    public OrderByIdSpecification(Guid id)
        :base(o=> o.Id == id)
    {
        AddInclude(o=> o.Items);
        AddInclude(o=> o.DeliveryMethod);
    }
}
