using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.Products;

namespace E_Commerce.Service.Services;
internal class ProductService(IUnitOfWork UnitOfWork, IMapper mapper) : IProductService
{
    public async Task<IEnumerable<BrandRespose>> GetBrandsAsync(CancellationToken cancellationToken = default)
    {
       var brands = await  UnitOfWork.GetRepository<ProductBrand, int>()
            .GetAllAysnc(cancellationToken);
        return mapper.Map<IEnumerable<BrandRespose>>(brands);
    }

    public async Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await UnitOfWork.GetRepository<Product,int>()
            .GetByIdASync(id, cancellationToken);
        return mapper.Map<ProductResponse>(product);
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await UnitOfWork.GetRepository<Product, int>()
     .GetAllAysnc(cancellationToken);
        return mapper.Map<IEnumerable<ProductResponse>>(products);
    }

    public async Task<IEnumerable<TypeRespose>> GetTypesAsync(CancellationToken cancellationToken = default)
    {
        var types = await UnitOfWork.GetRepository<ProductType, int>()
     .GetAllAysnc(cancellationToken);
        return mapper.Map<IEnumerable<TypeRespose>>(types);
    }
}
