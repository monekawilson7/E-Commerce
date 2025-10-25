using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.Basket;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.ApI.Controllers;
public class BasketsController (IBasketService basketService)
    : APIBaseController
{
    [HttpPost]
    public async Task<ActionResult<CustomerBasketDTO>> Update (CustomerBasketDTO basketDTO)
    {
        return Ok(await basketService.CreateOrUpdateAsync(basketDTO));
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasketDTO>> Get (string id)
    {
        return Ok(await basketService.GetByIdAsync(id));
    }

    [HttpDelete]
    public async Task<ActionResult> Delete (string id)
    {
        await basketService.DeleteAsync(id);
        return NoContent();
    }
}
