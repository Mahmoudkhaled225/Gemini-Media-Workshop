using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;

namespace RepositoryLayer.Repositories.Seeds;

public static class AdminSeed
{
    public static async Task SeedAdminAsync(ApplicationDbContext context)
    {
        if (await context.Users.AnyAsync(u => u.Email == "admin@gmail.com")) return;

        var user = new User
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            Password = "admin@123",
            Dob = new DateTime(2000, 7, 10),
            Phone = "+201555855529",
        };
        
        context.Users.Add(user);
        await context.SaveChangesAsync();
        
    }

}