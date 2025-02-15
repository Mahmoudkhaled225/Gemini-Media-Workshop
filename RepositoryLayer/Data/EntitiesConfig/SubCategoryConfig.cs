using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Data.EntitiesConfig;

public class SubCategoryConfig : IEntityTypeConfiguration<SubCategory>
{
    
public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SubCategory> builder)
    {
        builder.HasKey(sc => sc.Id); // Set primary key

        builder.Property(sc => sc.Name).IsRequired(); // Set Name as required

        builder.HasIndex(sc => sc.Name).IsUnique();
    }
}