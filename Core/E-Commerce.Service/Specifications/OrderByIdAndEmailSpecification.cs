using E_Commerce.Domain.Entities.OrderEntities;

namespace E_Commerce.Service.Specifications;
internal class OrderByIdAndEmailSpecification
    :BaseSpecification<Order>
{
    public OrderByIdAndEmailSpecification(string email, Guid id)
        :base(o=> o.Id == id && o.UserEmail == email)
    {
        AddInclude(o=> o.Items);
        AddInclude(o=> o.DeliveryMethod);
    }
}
