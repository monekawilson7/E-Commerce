
using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Domain.Entities.OrderEntities;
using E_Commerce.Presistence.AuthContext;
using E_Commerce.Presistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Text.Json;

namespace E_Commerce.Presistence.DbInitializers;
internal class DbInitializer(StoreDbContext dbContext,
    AuthDbContext authDbContext,
    RoleManager<IdentityRole> roleManager,
    UserManager<ApplicationUser> userManager,
    ILogger<DbInitializer> logger)
    : IDbInitializer
{
    public async Task InitializeAsync()
    {
        try
        {
            // Creare DB
            // Update-Database 
            if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
                await dbContext.Database.MigrateAsync();
            if (!dbContext.ProductBrands.Any())
            {
                var BrandsData = await File.ReadAllTextAsync("C:\\Users\\DELL\\source\\repos\\E-Commerce\\Infrastrucrure\\E-Commerce.Presistence\\Context\\DataSeed\\brands.json");
                // C: \Users\DELL\source\repos\E - Commerce\Infrastrucrure\E - Commerce.Presistence\Context\DataSeed
                //C:\Users\DELL\source\repos\E-Commerce\E-Commerce.Presistence\Context\SeedData\brands.json
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands is not null && Brands.Any())
                    dbContext.ProductBrands.AddRange(Brands);

                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.ProductTypes.Any())
            {
                var TypesData = await File.ReadAllTextAsync("C:\\Users\\DELL\\source\\repos\\E-Commerce\\Infrastrucrure\\E-Commerce.Presistence\\Context\\DataSeed\\types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types is not null && Types.Any())
                    dbContext.ProductTypes.AddRange(Types);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync("C:\\Users\\DELL\\source\\repos\\E-Commerce\\Infrastrucrure\\E-Commerce.Presistence\\Context\\DataSeed\\products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products is not null && Products.Any())
                    dbContext.Products.AddRange(Products);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.DeliveryMethods.Any())
            {
                var ProductsData = await File.ReadAllTextAsync("C:\\Users\\DELL\\source\\repos\\E-Commerce-Old\\Infrastrucrure\\E-Commerce.Presistence\\Context\\DataSeed\\delivery.json");
                var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(ProductsData);
                if (delivery is not null && delivery.Any())
                    dbContext.DeliveryMethods.AddRange(delivery);

                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message);
            throw;
        }

    }

    public async Task InitializeAuthDbAsync()
    {
        await authDbContext.Database.MigrateAsync();
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        }
        if (!userManager.Users.Any())
        {
            var SuperadminUser = new ApplicationUser
            {
                UserName = "SuperAdmin",
                Email = "SuperAdmin@gmail.com",
                DisplayName = "Super Admin",
                PhoneNumber = "0123456789",
            };
            var adminUser = new ApplicationUser
            {
                UserName = "Admin",
                Email = "Admin@gmail.com",
                DisplayName = "Admin",
                PhoneNumber = "0123456789",
            };
            await userManager.CreateAsync(SuperadminUser, "Pa$$w0rd");
            await userManager.CreateAsync(adminUser, "Pa$$w0rd");
            await userManager.AddToRoleAsync(SuperadminUser, "SuperAdmin");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

}

