using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Specifications;

public static class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;


        // Includes all expression-based includes
        query = specification.Includes.Aggregate(query,
            (current, include) => current.Include(include));
        // Include any string-based include statements
        query = specification.IncludeStrings.Aggregate(query,
            (current, include) => current.Include(include));

        
        
        // modify the IQueryable using the specification's criteria expression
        if (specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        // Apply ordering if expressions are set
        if (specification.OrderBy is not null)
            query = query.OrderBy(specification.OrderBy);
        
        if (specification.OrderByDescending is not null)
            query = query.OrderByDescending(specification.OrderByDescending);
        
        if (specification.GroupBy is not null)
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        
        // Apply paging if enabled
        if (specification.IsPagingEnabled)
            query = query.Skip(specification.Skip.Value).Take(specification.Take.Value);
        
        return query;

    }
}