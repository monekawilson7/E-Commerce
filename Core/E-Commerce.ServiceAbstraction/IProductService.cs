using E_Commerce.Shared.DataTransferObjects.Products;

namespace E_Commerce.ServiceAbstraction;
public interface IProductService
{
    Task <ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductResponse>> GetProductsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<BrandRespose>> GetBrandsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TypeRespose>> GetTypesAsync(CancellationToken cancellationToken = default);
}
