using DomainLayer.Interfaces;
using RepositoryLayer.Data;

namespace RepositoryLayer.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context; 
    public ICategoryRepository CategoryRepository { get; set; }
    public ISubcategoryRepository SubcategoryRepository { get; set; }
    public IProductRepository ProductRepository { get; set; }
    
    public IBundleRepository BundleRepository { get; set; }
    
    public IUserRepository UserRepository { get; set; }
    

    public UnitOfWork(ApplicationDbContext context, ICategoryRepository categoryRepository,
        ISubcategoryRepository subcategoryRepository, IProductRepository productRepository,
        IBundleRepository bundleRepository, IUserRepository userRepository)
    {
        _context = context;
        CategoryRepository = categoryRepository;
        SubcategoryRepository = subcategoryRepository;
        ProductRepository = productRepository;
        BundleRepository = bundleRepository;
        UserRepository = userRepository;
    }

    public async Task<int> Save() => 
        await _context.SaveChangesAsync();
    
    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}