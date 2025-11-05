namespace E_Commerce.Domain.Entities.Basket;
public class BasketItem
{
#nullable disable
    public int Id { get; set; } = default; // Product Id
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string PictureUrl { get; set; }

}
