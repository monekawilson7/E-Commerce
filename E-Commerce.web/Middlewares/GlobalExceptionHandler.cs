using E_Commerce.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_Commerce.web.Middlewares;

public class GlobalExceptionHandler (RequestDelegate next,
    ILogger<GlobalExceptionHandler> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var problem = new ProblemDetails
                {
                    Title = "Resource Not Found",
                    Detail = $"The requested resource {context.Request.Path} was not found.",
                    Status = StatusCodes.Status404NotFound,
                    Instance = context.Request.Path
                };
                await context.Response.WriteAsJsonAsync(problem);
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Something Went Wrong {Message}", ex.Message);
            var problem = new ProblemDetails
            {
                Title = "Error Processing the HTTP Request",
                Detail = ex.Message,
                Instance = context.Request.Path,
                Status = ex switch { 
                NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError,
                },

            };
            context.Response.StatusCode = problem.Status.Value;
            await context.Response.WriteAsJsonAsync(problem);

        }
    }
}

public static class GlobalExceptionHandlerExtensions
{
    public static WebApplication UseCutomExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();
        return app;
    }
}
