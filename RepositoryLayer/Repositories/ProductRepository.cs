using System.Linq.Expressions;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;

namespace RepositoryLayer.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
    // Task<IEnumerable<Product>> GetAll(Expression<Func<Product, bool>> predicate);
    public async Task<IEnumerable<Product>> GetAll(Expression<Func<Product, bool>> predicate)
    {
        return await _context.Set<Product>().Where(predicate).ToListAsync();
    }

    
    public async Task<Product?> GetWithInclude(int id)
    {
        return await _context.Products
            .Include(p => p.Categories)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    
        // Reload the entity to get the Sku generated by the trigger
        await _context.Entry(product).ReloadAsync(); 
    
        return product;
    }



}