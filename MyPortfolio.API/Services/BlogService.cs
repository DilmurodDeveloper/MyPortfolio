using MyPortfolio.API.Models;

namespace MyPortfolio.API.Services
{
    public class BlogService : IBlogService
    {
        private readonly List<BlogPost> posts = new()
    {
        new BlogPost
        {
            Id = 1,
            Title = "Welcome to My Blog",
            Content = "This is the first blog post.",
            PublishedAt = DateTime.UtcNow.AddDays(-10),
            ImageUrl = "/images/blog1.png"
        },
        new BlogPost
        {
            Id = 2,
            Title = "Blazor Tips and Tricks",
            Content = "Some useful tips for Blazor developers.",
            PublishedAt = DateTime.UtcNow.AddDays(-5),
            ImageUrl = "/images/blog2.png"
        }
    };

        public IEnumerable<BlogPost> GetAll() => posts;

        public BlogPost? GetById(int id) =>
            posts.FirstOrDefault(p => p.Id == id);
    }
}
