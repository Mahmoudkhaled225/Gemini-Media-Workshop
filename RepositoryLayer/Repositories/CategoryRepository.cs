using System.Linq.Expressions;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;

namespace RepositoryLayer.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Category>> GetAll(Expression<Func<Category, bool>> predicate)
    {
        return await _context.Set<Category>().Where(predicate).ToListAsync();
    }

    public async Task<Category?> GetWithInclude(int id)
    {
        return await _context.Categories
            .Include(c => c.SubCategories)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}