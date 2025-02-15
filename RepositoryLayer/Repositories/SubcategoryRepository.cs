using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;

namespace RepositoryLayer.Repositories;

public class SubcategoryRepository : GenericRepository<SubCategory>, ISubcategoryRepository
{
    private readonly ApplicationDbContext _context;

    public SubcategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SubCategory?> GetWithInclude(int id)
    {
        return await _context.SubCategories
            .Include(sc => sc.Category)
            .FirstOrDefaultAsync(sc => sc.Id == id);
    }
}