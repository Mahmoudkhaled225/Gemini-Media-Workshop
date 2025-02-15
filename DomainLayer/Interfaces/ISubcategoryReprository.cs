using DomainLayer.Entities;

namespace DomainLayer.Interfaces;

public interface ISubcategoryRepository : IGenericRepository<SubCategory>
{
    Task<SubCategory?> GetWithInclude(int id);
}