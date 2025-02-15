using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Entities;


[Table("categories")]
[Index(nameof(Name), IsUnique = true)]
public class Category : BaseEntity
{
    [Required] 
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [ForeignKey("ParentCategory")]
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    
    // public Img? Img { get; set; }
    
    public string? ImgUrl { get; set; }
    public string? PublicId { get; set; }

    // public List<string>? ImgUrls { get; set; }
    // public List<string>? PublicIds { get; set; }

    [JsonIgnore] 
    public List<SubCategory>? SubCategories { get; set; }
    

    public List<Product> Products { get; set; } = new List<Product>();

    public List<Bundle>? Bundles { get; set; }

}