using MyPortfolio.API.Models;

namespace MyPortfolio.API.Services
{
    public class BlogService : IBlogService
    {
        private readonly List<BlogPost> posts = new();

        public IEnumerable<BlogPost> GetAll() => posts;

        public BlogPost? GetById(int id) => posts.FirstOrDefault(p => p.Id == id);

        public void Add(BlogPost post)
        {
            post.Id = posts.Count + 1;
            post.PublishedAt = DateTime.UtcNow;
            posts.Add(post);
        }

        public void Update(BlogPost post)
        {
            var existing = GetById(post.Id);
            if (existing == null) return;

            existing.Title = post.Title;
            existing.Content = post.Content;
            existing.ImageUrl = post.ImageUrl;
        }

        public void Delete(int id)
        {
            var post = GetById(id);
            if (post != null) posts.Remove(post);
        }
    }
}
