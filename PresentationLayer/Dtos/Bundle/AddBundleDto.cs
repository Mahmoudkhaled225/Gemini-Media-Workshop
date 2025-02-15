using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Dtos.Bundle;

public class AddBundleDto
{
    [Required] [MinLength(2)] public string Name { get; set; }

    public string Description { get; set; }

    [Required] public decimal Price { get; set; }

    [Required] [Range(0.00, 100.00)] public double Discount { get; set; }

    [Required] public List<int> CategoriesId { get; set; }

    [Required] public List<int> ProductsId { get; set; }
}