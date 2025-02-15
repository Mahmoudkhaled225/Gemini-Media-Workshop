namespace PresentationLayer.Dtos.Product;


public class AddProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
    public string VideoUrl { get; set; }

    public List<int> CategoriesId { get; set; }
    public IFormFile Image { get; set; }
}