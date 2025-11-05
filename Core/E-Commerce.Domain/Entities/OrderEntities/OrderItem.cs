namespace E_Commerce.Domain.Entities.OrderEntities;
public class OrderItem : Entity<Guid>
{
    public ProductInOrderItem product { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid OrderId { get; set; }
    }

public class ProductInOrderItem
{
    public int ProductId { get; set; } = default; // Product Id
    public string Name { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;


}
