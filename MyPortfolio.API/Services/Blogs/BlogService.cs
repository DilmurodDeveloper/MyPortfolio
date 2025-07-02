using Microsoft.EntityFrameworkCore;
using MyPortfolio.API.Models;
using MyPortfolio.Shared.DTOs.Blogs;

namespace MyPortfolio.API.Services.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext db;

        public BlogService(AppDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<List<BlogDto>> GetAllAsync()
        {
            return await db.Blogs
                .OrderByDescending(b => b.PublishedAt)
                .Select(b => new BlogDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Content = b.Content,
                    ImageUrl = b.ImageUrl,
                    PublishedAt = b.PublishedAt
                })
                .ToListAsync();
        }

        public async Task<BlogDto?> GetByIdAsync(int id)
        {
            var blog = await db.Blogs.FindAsync(id);
            if (blog is null) return null;

            return new BlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                ImageUrl = blog.ImageUrl,
                PublishedAt = blog.PublishedAt
            };
        }

        public async Task<int> CreateAsync(CreateBlogDto dto)
        {
            var blog = new Blog
            {
                Title = dto.Title,
                Content = dto.Content,
                ImageUrl = dto.ImageUrl
            };

            db.Blogs.Add(blog);
            await db.SaveChangesAsync();
            return blog.Id;
        }

        public async Task<bool> UpdateAsync(int id, UpdateBlogDto dto)
        {
            var blog = await db.Blogs.FindAsync(id);
            if (blog is null) return false;

            blog.Title = dto.Title;
            blog.Content = dto.Content;
            blog.ImageUrl = dto.ImageUrl;

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var blog = await db.Blogs.FindAsync(id);
            if (blog is null) return false;

            db.Blogs.Remove(blog);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
