namespace E_Commerce.Shared.DataTransferObjects.UserOrder;
public record DeliveryMethodResponse
{
    public int Id { get; set; }
    public string ShortName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string DeliveryTime { get; set; } = default!;
    public decimal Cost { get; init; }
}
