namespace E_Commerce.Shared.DataTransferObjects.Products;
public record ProductResponse(int Id, string Name, string Description, string PictureUrl, 
    decimal Price, string Brand, string Type);

