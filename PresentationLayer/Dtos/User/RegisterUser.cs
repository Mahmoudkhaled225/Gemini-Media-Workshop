using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Dtos.User;

public class RegisterUser
{
    [MinLength(2)]
    [Required]
    public string UserName { get; set; } = string.Empty;

    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;
    
    // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")]
    // here don't put on it any validation
    // because we will hash it,but we will validate it in the service
    [Required]
    [MinLength(3)]

    public string Password { get; set; } = string.Empty;

    
    [Required]
    public DateTime Dob { get; set;} 
    
    [RegularExpression(@"^\+201[0125][0-9]{9}$")]
    public string? Phone { get; set; }  


}