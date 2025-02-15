
namespace ServiceLayer.Abstractions;

public interface IAuthService
{
    int? GetUserIdFromToken(string token);
}