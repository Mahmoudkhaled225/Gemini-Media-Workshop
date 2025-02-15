using System.Linq.Expressions;
using DomainLayer.Entities;

namespace DomainLayer.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    
    Task<IEnumerable<Category>> GetAll(Expression<Func<Category, bool>> predicate);

    //get with include subcategories
    Task<Category?> GetWithInclude(int id);
}