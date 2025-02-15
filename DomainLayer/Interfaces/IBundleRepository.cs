using System.Linq.Expressions;
using DomainLayer.Entities;

namespace DomainLayer.Interfaces;


public interface IBundleRepository : IGenericRepository<Bundle>
{
    
    Task<IEnumerable<Bundle>> GetAll(Expression<Func<Bundle, bool>> predicate);
    Task<Bundle?> GetWithInclude(int id);
    
    
}