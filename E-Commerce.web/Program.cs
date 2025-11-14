using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastrucrure.Service;
using E_Commerce.Presistence.DependencyInjection;
using E_Commerce.Service.Depe.ndencyInjection;
using E_Commerce.web.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
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
            builder.Services.AddApplicationServices()
                .AddPersistenceServices(builder.Configuration)
                .AddInfrastructureServices(builder.Configuration);
            builder.Services.Configure<JWTOtions>(builder.Configuration.GetSection(JWTOtions.SectionName));
            builder.Services.AddPersistenceServices(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Enter 'Bearer' [space] and then your token in the text input below."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
            } );
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

            builder.Services.AddAuthentication(Options => { 
            Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(Options => {
                    var jwt = builder.Configuration.GetSection(JWTOtions.SectionName).Get<JWTOtions>();
                    Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    { 
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                    };
                });

            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.InitializeAsync();
            await initializer.InitializeAuthDbAsync();

            //app.UseMiddleware<GlobalExceptionHandler>();
            //app.UseCutomExceptionHandler();
            app.UseExceptionHandler("/Auth");

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
            app.UseAuthentication();
            app.UseAuthorization();


           // app.MapControllers();

            app.Run();
        }
    }
}
