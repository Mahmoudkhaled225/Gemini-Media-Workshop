using DomainLayer.Entities;
using DomainLayer.Specifications;

namespace RepositoryLayer.Specifications;

public class SubCategorySpec : BaseSpecification<SubCategory>
{
    
    public SubCategorySpec(int? pageNumber, int? pageSize)
    {
        Includes.Add(x => x.Category);
        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
    }    
    
    public SubCategorySpec(int id) : base(c => c.Id == id)
    {
        Includes.Add(c => c.Category);
        AddOrderBy(c => c.Name);
    }   
    
    
    
    
    
}