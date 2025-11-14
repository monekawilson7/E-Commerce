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
    public  async Task<ActionResult<OrderResponse>> Create(OrderRequest request,CancellationToken cancellationToken)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await orderService.CreateAsync(request, email, cancellationToken);
        return HandelResult(result);
    }

    [HttpGet("DelivryMethods")]
    public async Task<ActionResult<IEnumerable<DeliveryMethodResponse>>> GetDeliveryMethods(CancellationToken cancellationToken)
    {
       var methods = await orderService.GetDeliveryMethodsAsync(cancellationToken);
         return Ok(methods);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> Get(Guid id, CancellationToken cancellationToken )
    {
        var email= User.FindFirstValue(ClaimTypes.Email);
        var result = await orderService.GetOrderAsync(email, id, cancellationToken);
        return HandelResult(result);
    }
    [HttpGet]
    public async Task<ActionResult<OrderResponse>> GetAll(CancellationToken cancellationToken)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await orderService.GetAllAsync(email, cancellationToken);
        return Ok(result);
    }
}
