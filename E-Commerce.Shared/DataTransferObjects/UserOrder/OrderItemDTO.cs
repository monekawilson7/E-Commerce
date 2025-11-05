namespace E_Commerce.Shared.DataTransferObjects.UserOrder;
public class OrderItemDTO
{
    public Guid Id { get; set; }
    public int ProductId { get; set; } = default; // Product Id
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid OrderId { get; set; }
}
