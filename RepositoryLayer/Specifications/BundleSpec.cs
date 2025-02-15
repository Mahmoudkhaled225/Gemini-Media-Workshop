using DomainLayer.Entities;
using DomainLayer.Specifications;

namespace RepositoryLayer.Specifications;

public class BundleSpec : BaseSpecification<Bundle>
{
    public BundleSpec(int? pageNumber, int? pageSize)
    {
        Includes.Add(x => x.Products);
        Includes.Add(x => x.Categories);
        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
    }    
    
    public BundleSpec(int id) : base(c => c.Id == id)
    {
        Includes.Add(c => c.Products);
        Includes.Add(c => c.Categories);
        AddOrderBy(c => c.Name);
    }

}