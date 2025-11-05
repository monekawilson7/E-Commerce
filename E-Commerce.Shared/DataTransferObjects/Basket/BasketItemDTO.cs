namespace E_Commerce.Shared.DataTransferObjects.Basket;

public class BasketItemDTO
{
#nullable disable
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string PictureUrl { get; set; }
}
