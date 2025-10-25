using E_Commerce.Presistence.Context;
using E_Commerce.Presistence.DbInitializers;
using E_Commerce.Presistence.Repositories;
using E_Commerce.Presistence.Services;
using E_Commerce.ServiceAbstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace E_Commerce.Presistence.DependencyInjection;
public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration )
    {
        services.AddScoped<ICashService, CashService>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddSingleton<IConnectionMultiplexer>(cfg => {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RadisConnection")!);
        });
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connection = configuration.GetConnectionString("SQLConnection");
            options.UseSqlServer(connection);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDbInitializer,DbInitializer>();
        return services;
    }
}