using E_Commerce.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace E_Commerce.Presentation.ApI.Attributes;
internal class redisCashAttribute (int durationInMin = 2)
    : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
       var cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
        string Key = GenerateCashKey(context.HttpContext.Request);
        var cashValue = await cashService.GetAsync(Key);
        if (cashValue is not null)
        {
            context.Result = new ContentResult
            {
                Content = cashValue,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
            return;
        }
        var actionExecutedContext = await next.Invoke();
        var result = actionExecutedContext.Result;
        if (result is OkObjectResult okObjectResult)
        {
            await cashService.SetAsync(Key, okObjectResult.Value!, TimeSpan.FromMinutes(durationInMin));
        }


    }

    private static string GenerateCashKey(HttpRequest request)
    {
        var sb = new StringBuilder();
        foreach (var KVP in request.Query.OrderBy(q=>q.Key))
        {
            sb.Append($"{KVP.Key}-{KVP.Value}-");
        }
        return sb.ToString().Trim('-');
    }
}
