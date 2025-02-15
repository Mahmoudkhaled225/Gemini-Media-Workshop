namespace PresentationLayer.Dtos.User;

public class ReturnedUserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } = "*************";
    public string? Phone { get; set; }
    public int Age { get; set; }
    
}