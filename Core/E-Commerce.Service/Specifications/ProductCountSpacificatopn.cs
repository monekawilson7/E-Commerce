using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.DataTransferObjects.Products;
using System.Linq.Expressions;

namespace E_Commerce.Service.Specifications;
internal sealed class ProductCountSpacificatopn(ProductQueryParameters parameters) 
    : BaseSpecification<Product>(CreateCriteria(parameters))
{
    private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters) 
    { 
    return p => (!parameters.BrandID.HasValue || p.BrandId == parameters.BrandID) &&
        (!parameters.TypeID.HasValue || p.TypeId == parameters.TypeID) &&
        (string.IsNullOrWhiteSpace(parameters.Search) || p.Name.Contains(parameters.Search));   
    }
}
