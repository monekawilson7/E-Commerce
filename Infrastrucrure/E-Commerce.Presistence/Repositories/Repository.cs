
using E_Commerce.Domain.Entities;
using E_Commerce.Presistence.Context;

namespace E_Commerce.Presistence.Repositories;
internal class Repository<TEntity, TKey>(StoreDbContext dbContext) :
    IRepository<TEntity, TKey>
   where TEntity : Entity<TKey>
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    public void Add(TEntity entity)
    => _dbSet.Add(entity);

    public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .ApplySpecification(specification)
            .CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAysnc( ISpecification<TEntity> specification,CancellationToken cancellationToken = default)
        => await _dbSet.ApplySpecification(specification).ToListAsync(cancellationToken);

    public async Task<IEnumerable<TEntity>> GetAllAysnc(CancellationToken cancellationToken = default)
      => await dbContext.Set<TEntity>().ToListAsync(cancellationToken);

    public async Task<TEntity?> GetASync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .ApplySpecification(specification)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdASync(TKey id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().FindAsync(id, cancellationToken);
    }

    public void Remove(TEntity entity)
      => dbContext.Set<TEntity>().Remove(entity);


    public void Update(TEntity entity) 
        => dbContext.Set<TEntity>().Update(entity);
}
