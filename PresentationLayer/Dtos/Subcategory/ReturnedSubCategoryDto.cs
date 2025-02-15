namespace PresentationLayer.Dtos.Subcategory;

public class ReturnedSubCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ReturnedCategoryForSubCate Category { get; set; }
}

