using DomainLayer.Entities;
using DomainLayer.Specifications;

namespace RepositoryLayer.Specifications;

public class CategorySpec : BaseSpecification<Category>
{

    public CategorySpec(int? pageNumber, int? pageSize)
    {
        Includes.Add(x => x.SubCategories);
        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
    }    
    
    public CategorySpec(int id) : base(c => c.Id == id)
    {
        Includes.Add(c => c.ParentCategory);
        Includes.Add(c => c.SubCategories);
        AddOrderBy(c => c.Name);
    }

}