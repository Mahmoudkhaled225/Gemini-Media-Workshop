using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Data.EntitiesConfig;

public class BundleConfig: IEntityTypeConfiguration<Bundle>
{
    public void Configure(EntityTypeBuilder<Bundle> builder)
    {
        builder.HasKey(b => b.Id);
        
        
        
        builder.Property(b => b.Name).IsRequired();
        
        builder.HasIndex(b => b.Name).IsUnique(true);
        
        builder.Property(b => b.Description).HasMaxLength(30);
        
        builder.Property(b => b.Price).HasColumnType("decimal(18,2)").IsRequired();
        
        builder.Property(b => b.Discount).IsRequired();
        
        builder.HasMany(b => b.Categories)
            .WithMany(c => c.Bundles)
            .UsingEntity(j => j.ToTable("BundleCategories"));
        
        builder.HasMany(b => b.Products)
            .WithMany(p => p.Bundles)
            .UsingEntity(j => j.ToTable("BundleProducts"));
        
        
    }

    
}