using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Contracts;
public interface IRepository<TEntity,TKey>
    where TEntity : Entity<TKey>
{
    public void Add (TEntity entity);
    public void Remove (TEntity entity);
    public void Update (TEntity entity);
    Task<TEntity?> GetByIdASync(TKey id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAysnc(CancellationToken cancellationToken = default);
}
