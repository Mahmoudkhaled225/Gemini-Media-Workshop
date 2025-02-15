using System.Reflection;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Data;

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext() { }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
    

    
    public DbSet<Bundle> Bundles { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<SubCategory> SubCategories { get; set; } = null!;
    
    public DbSet<Img> Imgs { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    
    

}