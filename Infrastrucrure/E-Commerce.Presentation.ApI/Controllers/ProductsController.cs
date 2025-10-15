using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.ApI.Controllers;

public class ProductsController (IProductService service)
    : APIBaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts(CancellationToken cancellationToken = default) 
    {
        var response = await service.GetProductsAsync(cancellationToken);
        return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> Get(int id, CancellationToken cancellationToken = default)
    {
        var response = await service.GetByIdAsync(id,cancellationToken);
        return Ok(response);
    }
    [HttpGet("Brands")]
    public async Task<ActionResult<IEnumerable<BrandRespose>>> GetBrands(CancellationToken cancellationToken = default)
    {
        var response = await service.GetBrandsAsync(cancellationToken);
        return Ok(response);
    }
    [HttpGet("Types")]
    public async Task<ActionResult<IEnumerable<BrandRespose>>> GetTypes(CancellationToken cancellationToken = default)
    {
        var response = await service.GetTypesAsync(cancellationToken);
        return Ok(response);
    }
}
