namespace E_Commerce.Domain.Entities.OrderEntities;
public class Order : Entity<Guid>
{
    public string UserEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public decimal Subtotal { get; set; }
    public string PaymentIntentId { get; set; } = string.Empty;
    public DeliveryMethod DeliveryMethod { get; set; } =default!;
    public int? DeliveryMethodId { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.pending;
    public ICollection<OrderItem> Items { get; set; } = [];
    public OrderAddress Address { get; set; } = default!;
    public string PaymentIntentName { get; set; } = string.Empty;
}
public class OrderAddress
{
    public string FristName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string Street { get; set; } = default!;
}
