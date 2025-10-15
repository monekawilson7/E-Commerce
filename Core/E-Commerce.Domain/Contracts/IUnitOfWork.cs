using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Domain.Contracts;
public interface IUnitOfWork
{
     //IRepository<Product, int> Products { get; set; }
     //IRepository<Product, int> Products { get; set; }
     //IRepository<Product, int> Products { get; set; }
     Task<int> SaveChangesAysnc(CancellationToken cancellationToken = default);
    IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : Entity<TKey>;
}
