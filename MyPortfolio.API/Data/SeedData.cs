using MyPortfolio.API.Models;

namespace MyPortfolio.API.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword("@admin1234!");

                var admin = new User
                {
                    Username = "DilmurodDev",
                    PasswordHash = passwordHash,
                    Role = "Admin"
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
