using E_Commerce.Domain.Entities.OrderEntities;

namespace E_Commerce.Service.Specifications;
internal class OrderByEmailSpecifications
    :BaseSpecification<Order>
{
    public OrderByEmailSpecifications(string email)
        :base(o=> o.UserEmail == email)
    {
       
        AddInclude(o=> o.Items);
        AddInclude(o=> o.DeliveryMethod);

        AddOrderBy(o=> o.OrderDate);
    }
}
