using E_Commerce.Domain.Entities;
using E_Commerce.Presistence.Context;

namespace E_Commerce.Presistence.Repositories;
internal class UnitOfWork(StoreDbContext dbContext) 
    : IUnitOfWork
{

    private readonly Dictionary<string , object> _repositories =[];
    public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : Entity<TKey>
    {
        var typeName = typeof(TEntity).Name;
        if (_repositories.ContainsKey(typeName))
            return (_repositories[typeName]as IRepository<TEntity, TKey>)!;

        var repo =  new Repository<TEntity, TKey>(dbContext);
        _repositories.Add(typeName, repo);
        return repo;
    }

    public async Task<int> SaveChangesAysnc(CancellationToken cancellationToken = default)
        => await dbContext.SaveChangesAsync(cancellationToken);
}
