
using E_Commerce.Presistence.Context;
using System.Data;
using System.Text.Json;

namespace E_Commerce.Presistence.DbInitializers;
internal class DbInitializer(ApplicationDbContext dbContext) 
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
            //if (!dbContext.ProductTypes.Any())
            //{
            //    var TypesData = await File.ReadAllTextAsync("../E-Commerce.Presistence/SeedData/types.json");
            //    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
            //    if (Types is not null && Types.Any())
            //        dbContext.ProductTypes.AddRange(Types);
            //    await dbContext.SaveChangesAsync();
            //}
            if (!dbContext.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync("C:\\Users\\DELL\\source\\repos\\E-Commerce\\Infrastrucrure\\E-Commerce.Presistence\\Context\\DataSeed\\products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products is not null && Products.Any())
                    dbContext.Products.AddRange(Products);
                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
