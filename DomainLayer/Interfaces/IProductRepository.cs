using System.Linq.Expressions;
using DomainLayer.Entities;

namespace DomainLayer.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<Product?> GetWithInclude(int id);
    
    Task<IEnumerable<Product>> GetAll(Expression<Func<Product, bool>> predicate);
    
    Task<Product> AddProductAsync(Product product);

}