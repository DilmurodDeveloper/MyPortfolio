using MyPortfolio.Shared.DTOs.Blogs;

namespace MyPortfolio.API.Services.Blogs
{
    public interface IBlogService
    {
        Task<List<BlogDto>> GetAllAsync();
        Task<BlogDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateBlogDto dto);
        Task<bool> UpdateAsync(int id, UpdateBlogDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
