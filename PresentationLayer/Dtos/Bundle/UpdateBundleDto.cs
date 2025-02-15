namespace PresentationLayer.Dtos.Bundle;

public class UpdateBundleDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<int>? CategoriesId { get; set; }
    public List<int>? ProductsId { get; set; }
    public double? Discount { get; set; }
    public decimal? Price { get; set; }

}