using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Service.Specifications;
internal class GetproductsByIdsSpecification (List<int> ids)
    : BaseSpecification<Product>(p=>ids.Contains(p.Id) );
