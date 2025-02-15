using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Data.EntitiesConfig;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id); // Set primary key

        builder.Property(p => p.Name).IsRequired(); // Set Name as required

        builder.HasIndex(p => p.Name).IsUnique(false);
        
        builder.Property(p => p.Description)
            .HasMaxLength(30); // Set max length of Description to 30 characters

        builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired(); // Set precision for Price

        builder.HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity(j => j.ToTable("ProductCategories"));

        builder.Property(p => p.SKU).HasDefaultValueSql("(YOUR_DEFAULT_SQL_HERE)").ValueGeneratedOnAddOrUpdate();

        
        builder.ToTable(t => t.UseSqlOutputClause(false));

    }
    
}