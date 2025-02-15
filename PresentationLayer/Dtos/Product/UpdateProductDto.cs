namespace PresentationLayer.Dtos.Product;

public class UpdateProductDto
{
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal ?Price { get; set; }
    public IFormFile? Image { get; set; }
    public List<int> CategoriesId { get; set; } = new List<int>();
}