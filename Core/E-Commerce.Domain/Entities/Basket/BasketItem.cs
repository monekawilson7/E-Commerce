namespace E_Commerce.Domain.Entities.Basket;
public class BasketItem
{
#nullable disable
    public string Id { get; set; } = default;
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string PictureUrl { get; set; }

}
