using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DomainLayer.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Abstractions;

namespace ServiceLayer.Concretes;

public class AuthService : IAuthService
{
    private readonly Jwt _jwt;    
    private readonly IUnitOfWork _unitOfWork;
    public AuthService(IOptions<Jwt> jwt, IUnitOfWork unitOfWork)
    {
        this._jwt = jwt.Value;        
        this._unitOfWork = unitOfWork;
    }
    
    private ClaimsPrincipal GetTokenPrincipal(string token)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));
        var validation = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
            ValidateIssuer = true,
            ValidIssuer = _jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwt.Audience,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
    }
    
    public int? GetUserIdFromToken(string token)
    {
        var principal = GetTokenPrincipal(token);
        var userId = principal?.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
        return userId != null ? int.Parse(userId) : null;
    }


}