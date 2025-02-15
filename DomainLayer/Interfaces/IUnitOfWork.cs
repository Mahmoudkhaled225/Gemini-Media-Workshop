namespace DomainLayer.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    
    ICategoryRepository CategoryRepository { get; set; }
    
    ISubcategoryRepository SubcategoryRepository { get; set; }
    
    IProductRepository ProductRepository { get; set; }
    
    IBundleRepository BundleRepository { get; set; }
    
    IUserRepository UserRepository { get; set; }
    
    Task<int> Save();
}