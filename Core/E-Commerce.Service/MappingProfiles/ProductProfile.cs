using AutoMapper;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.DataTransferObjects.Products;

namespace E_Commerce.Service.MappingProfiles;
internal class ProductProfile :Profile
{
    public ProductProfile() {
        CreateMap<Product, ProductResponse>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.ProductType.Name));
        CreateMap<ProductBrand, BrandRespose>();
        CreateMap<ProductType, TypeRespose>();

    }
}
