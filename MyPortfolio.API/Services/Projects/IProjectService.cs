using MyPortfolio.Shared.DTOs.Projects;

namespace MyPortfolio.API.Services.Projects
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAllAsync();
        Task<ProjectDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateProjectDto dto);
        Task<bool> UpdateAsync(int id, UpdateProjectDto dto);
        Task<bool> DeleteAsync(int id);
    }
}