using E_Commerce.Infrastrucrure.Service;
using E_Commerce.Service.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Infrastrucrure.Service;
public static class DependancyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration
        )
    {
        //services.Configure<JWTOtions>(configuration.GetSection(""));
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}
    