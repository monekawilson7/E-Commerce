using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.DataTransferObjects.Products;

namespace E_Commerce.Service.Specifications;
internal class ProductWithBrandTypeSpecification : BaseSpecification<Product>
{
    public ProductWithBrandTypeSpecification(ProductQueryParameters parameters)
        : base(p=> (!parameters.BrandID.HasValue || p.BrandId == parameters.BrandID ) && 
        (!parameters.TypeID.HasValue || p.TypeId == parameters.TypeID) &&
        (string.IsNullOrWhiteSpace(parameters.Search)||p.Name.Contains(parameters.Search)))
    {
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
        ApplyPagination(parameters.PageSize , parameters.PageIndex);

        switch (parameters.sort)
        {
            case (int)ProductSortOptions.NameAsending:
                AddOrderBy(p => p.Name);
                break;
            case (int)ProductSortOptions.NameDesending:
                AddOrderByDesc(p => p.Name);
                break;
            case (int)ProductSortOptions.PriceAsending:
                AddOrderBy(p => p.Price);
                break;
            case (int)ProductSortOptions.PriceDesending:
                AddOrderByDesc(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }
    public ProductWithBrandTypeSpecification(int id)
     : base(p=> p.Id == id)
    {
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
    }
}
