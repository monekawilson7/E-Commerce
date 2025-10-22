using E_Commerce.Service.Specifications;
namespace E_Commerce.Presistence.Repositories;
public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> ApplySpecification<TEntity>(this IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> specification)
        where TEntity : class
    {
        var query = inputQuery;

        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        foreach (var includeExpression in specification.Includes)
        {
            query = query.Include(includeExpression);
        } 

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDesc is not null)
        {
            query = query.OrderByDescending(specification.OrderByDesc);
        }

        if (specification.IsPaginated)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }
        return query;
    }

}
