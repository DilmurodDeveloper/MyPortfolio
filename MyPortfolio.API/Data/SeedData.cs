using Microsoft.EntityFrameworkCore;
using MyPortfolio.API.Models;

namespace MyPortfolio.API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Username = "Dilmurod",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("@admin1234!"),
                    Role = "Admin"
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
