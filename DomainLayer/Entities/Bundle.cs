using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Entities;


[Table("bundles")]
[Index(nameof(Name), IsUnique = true)]

public class Bundle : BaseEntity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    [Range(0.00, 100.00)]
    public double Discount { get; set; }
    
    public List<Category> Categories { get; set; }
    
    public List<Product> Products { get; set; }

    
}