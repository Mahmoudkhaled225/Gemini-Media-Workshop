namespace PresentationLayer.Dtos.Product;

public class ReturnedProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? VideoUrl { get; set; }
    public string? ImgUrl { get; set; }
    public string? PublicId { get; set; }
    public string? SKU { get; set; } = string.Empty;
    public List<CatergoryDto> Categories { get; set; }
}
