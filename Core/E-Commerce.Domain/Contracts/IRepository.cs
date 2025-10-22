using E_Commerce.Domain.Entities;
using E_Commerce.Presistence.Repositories;

namespace E_Commerce.Domain.Contracts;
public interface IRepository<TEntity,TKey>
    where TEntity : Entity<TKey>
{
    public void Add (TEntity entity);
    public void Remove (TEntity entity);
    public void Update (TEntity entity);
    Task<TEntity?> GetByIdASync(TKey id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetASync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAysnc(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAysnc(ISpecification<TEntity> specification,CancellationToken cancellationToken = default);
    Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}
