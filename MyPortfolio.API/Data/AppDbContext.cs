using Microsoft.EntityFrameworkCore;
using MyPortfolio.API.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
}
