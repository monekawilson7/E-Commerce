using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.UserOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.ApI.Controllers;
[Authorize]
public class OrdersController (IOrderService orderService)
    : APIBaseController
{
    [HttpPost]
    public  async Task<ActionResult<OrderResponse>> Create(OrderRequest request)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await orderService.CreateAsync(request, email);
        return HandelResult(result);
    }
}
