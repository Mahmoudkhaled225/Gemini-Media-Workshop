using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Dtos.User;

public class LoginUser
{
    
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}