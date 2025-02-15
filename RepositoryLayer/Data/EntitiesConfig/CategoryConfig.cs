using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Data.EntitiesConfig;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id); // Set primary key

        builder.Property(c => c.Name).IsRequired(); // Set Name as required

        builder.HasIndex(c => c.Name).IsUnique();
        
        builder.Property(c => c.Description)
            .HasMaxLength(30); // Set max length of Description to 30 characters

    }
    
}