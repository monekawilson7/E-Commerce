using E_Commerce.ServiceAbstraction.Common;
using E_Commerce.Shared.DataTransferObjects;
using E_Commerce.Shared.DataTransferObjects.Products;

namespace E_Commerce.ServiceAbstraction;
public interface IProductService
{
    Task <Result<ProductResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PaginatedResult<ProductResponse>> GetProductsAsync(ProductQueryParameters parameters,CancellationToken cancellationToken = default);
    Task<IEnumerable<BrandRespose>> GetBrandsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TypeRespose>> GetTypesAsync(CancellationToken cancellationToken = default);
}
