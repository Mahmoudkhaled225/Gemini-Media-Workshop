using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Entities;

[Table("subcategories")]

[Index(nameof(Name), IsUnique = true)]
public class SubCategory : BaseEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;
    
    // one-to-many relationship with user
    public int CategoryId { get; set; }
    [JsonIgnore] 
    public Category Category { get; set; }
    
}