using E_Commerce.Domain.Contracts;
using E_Commerce.Presistence.DependencyInjection;
using E_Commerce.Service.Depe.ndencyInjection;
using E_Commerce.web.Middlewares;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace E_Commerce.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext => 
                {
                    var errors = ActionContext.ModelState.Where(x => x.Value.Errors.Count>0)
                    .ToDictionary(x=>x.Key,x=>x.Value.Errors.Select(e=>e.ErrorMessage).ToArray());
                    var problem = new ProblemDetails
                    {
                        Title = "One or More Validation Errors Occurred",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "See the errors property for details.",
                       Extensions = { { "errors", errors } }
                    };
                    return new BadRequestObjectResult(problem);
                };
            });
                
            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.InitializeAsync();

            //app.UseMiddleware<GlobalExceptionHandler>();
            //app.UseCutomExceptionHandler();
            app.UseExceptionHandler();

            //app.Use(async(context , next) => 
            //{
            //    try
            //    {
            //        await next.Invoke(context);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //        await context.Response.WriteAsJsonAsync(new {
            //            statusCode = StatusCodes.Status500InternalServerError,
            //            Message = ex.Message
            //        });
            //    }


            //});


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


           // app.MapControllers();

            app.Run();
        }
    }
}
