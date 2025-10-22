using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Service.Specifications;
using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects;
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
            .GetASync(new ProductWithBrandTypeSpecification(id) , cancellationToken);
        return mapper.Map<ProductResponse>(product);
    }

    public async Task<PaginatedResult<ProductResponse>> GetProductsAsync(ProductQueryParameters parameters, CancellationToken cancellationToken = default)
    {
        var spec = new ProductWithBrandTypeSpecification(parameters);
        var data = await UnitOfWork.GetRepository<Product, int>()
     .GetAllAysnc(spec,cancellationToken);

        var totalCount = await UnitOfWork.GetRepository<Product,int>()
            .CountAsync(new ProductCountSpacificatopn(parameters), cancellationToken);
        var products = mapper.Map<IEnumerable<ProductResponse>>(data);
        return new (parameters.PageIndex, products.Count(), totalCount, products);
    }

    public async Task<IEnumerable<TypeRespose>> GetTypesAsync(CancellationToken cancellationToken = default)
    {
        var types = await UnitOfWork.GetRepository<ProductType, int>()
     .GetAllAysnc(cancellationToken);
        return mapper.Map<IEnumerable<TypeRespose>>(types);
    }
}
