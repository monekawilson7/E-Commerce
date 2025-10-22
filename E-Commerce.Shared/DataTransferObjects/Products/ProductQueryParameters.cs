namespace E_Commerce.Shared.DataTransferObjects.Products;
public class ProductQueryParameters
{
    private const int MaxPageSize = 10;
    private int DefaultPageSize = 5;
    public int? BrandID { get; set; }
    public int? TypeID { get; set; }
    public string? Search { get; set; }
    public int sort { get; set; }
    public ProductSortOptions Sort { get; set; }
    private int pageSize;
    public int PageSize {
        get=> pageSize;
        set=> pageSize= value>MaxPageSize ? MaxPageSize:
            value< DefaultPageSize ? DefaultPageSize :value; 
    }
    public int PageIndex { get; set; } = 1;
}

public enum ProductSortOptions
{
   NameAsending = 1,
    NameDesending = 2,
    PriceAsending = 3,
    PriceDesending = 4
}

