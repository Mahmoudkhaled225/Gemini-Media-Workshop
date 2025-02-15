namespace PresentationLayer.Dtos.Category;

public class UpdateCategoryDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? Image { get; set; }
    public int? ParentCategoryId { get; set; }
}