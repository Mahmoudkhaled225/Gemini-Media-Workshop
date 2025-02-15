namespace PresentationLayer.Dtos.Category;

public class AddCategoryDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int? ParentCategoryId { get; set; }
    
    public IFormFile? Image { get; set; }
    // public List<IFormFile>? ImgFiles { get; set; }
    
    // public List<AddSubCategoryDto>? SubCategories { get; set; }

}