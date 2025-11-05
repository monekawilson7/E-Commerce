using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Presistence.AuthContext;
using E_Commerce.Presistence.Context;
using E_Commerce.Presistence.DbInitializers;
using E_Commerce.Presistence.Repositories;
using E_Commerce.Presistence.Services;
using E_Commerce.ServiceAbstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace E_Commerce.Presistence.DependencyInjection;
public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration )
    {
        services.AddDbContext<AuthDbContext>(Options => {
            Options.UseSqlServer(configuration.GetConnectionString("AuthConnection"));
        });
        services.AddScoped<ICashService, CashService>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddSingleton<IConnectionMultiplexer>(cfg => {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RadisConnection")!);
        });
        services.AddDbContext<StoreDbContext>(options =>
        {
            var connection = configuration.GetConnectionString("SQLConnection");
            options.UseSqlServer(connection);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDbInitializer,DbInitializer>();
        ConfigureIdentity(services,configuration);
        return services;
    }

    private static void ConfigureIdentity (IServiceCollection services, IConfiguration configuration )
    {
        services.AddIdentityCore<ApplicationUser>(cfg=> { 
        cfg.Password.RequireDigit = false;
        cfg.Password.RequireLowercase = false;
            cfg.Password.RequireUppercase = false;
            cfg.Password.RequireNonAlphanumeric = false;
        }).AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AuthDbContext>();
    }
}