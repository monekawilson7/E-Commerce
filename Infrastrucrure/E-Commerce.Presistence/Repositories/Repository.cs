
using E_Commerce.Domain.Entities;
using E_Commerce.Presistence.Context;

namespace E_Commerce.Presistence.Repositories;
internal class Repository<TEntity, TKey>(ApplicationDbContext dbContext) :
    IRepository<TEntity, TKey>
   where TEntity : Entity<TKey>
{
    public void Add(TEntity entity)
    => dbContext.Set<TEntity>().Add(entity);

    public async Task<IEnumerable<TEntity>> GetAllAysnc(CancellationToken cancellationToken = default)
        => await dbContext.Set<TEntity>().ToListAsync(cancellationToken);

    public async Task<TEntity?> GetByIdASync(TKey id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().FindAsync(id, cancellationToken);
    }

    public void Remove(TEntity entity)
      => dbContext.Set<TEntity>().Remove(entity);


    public void Update(TEntity entity) 
        => dbContext.Set<TEntity>().Update(entity);
}
