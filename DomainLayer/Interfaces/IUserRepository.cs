using DomainLayer.Entities;

namespace DomainLayer.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    
    Task<User?> GetUserByEmail(string email);
    

    Task<User?> GetUserByEmailAndPassword(string email, string password);
    
}