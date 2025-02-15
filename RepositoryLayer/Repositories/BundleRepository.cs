using System.Linq.Expressions;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;

namespace RepositoryLayer.Repositories;


public class BundleRepository : GenericRepository<Bundle>, IBundleRepository
{
    private readonly ApplicationDbContext _context;

    public BundleRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Bundle>> GetAll(Expression<Func<Bundle, bool>> predicate)
    {
        return await _context.Set<Bundle>().Where(predicate).ToListAsync();
    }

    public async Task<Bundle?> GetWithInclude(int id)
    {
        return await _context.Bundles
            .Include(b => b.Products)
            .Include(b=>b.Categories)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

}