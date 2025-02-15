using PresentationLayer.Dtos.Product;

namespace PresentationLayer.Dtos.Bundle;

public class ReturnedBundleDto
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public double Discount { get; set; }
    public List<CatergoryDto> Categories { get; set; }
    public List<ProductDto> Products { get; set; }

    
}