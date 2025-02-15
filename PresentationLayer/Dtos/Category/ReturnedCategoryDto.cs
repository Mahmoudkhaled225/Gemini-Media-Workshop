using PresentationLayer.Dtos.Subcategory;

namespace PresentationLayer.Dtos.Category;

public class ReturnedCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImgUrl { get; set; }
    public string PublicId { get; set; }
    public ReturnedParentCategoryDto? ParentCategory { get; set; }
    public List<ReturnSubCategoryDto>? SubCategories { get; set; }
}