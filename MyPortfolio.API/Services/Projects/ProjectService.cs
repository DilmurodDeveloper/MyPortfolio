using Microsoft.EntityFrameworkCore;
using MyPortfolio.Shared.DTOs.Projects;
using MyPortfolio.Shared.Models;

namespace MyPortfolio.API.Services.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext dbContext;

        public ProjectService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ProjectDto>> GetAllAsync()
        {
            return await dbContext.Projects
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    GithubLink = p.GithubLink,
                    ImageUrl = p.ImageUrl,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var p = await dbContext.Projects.FindAsync(id);
            if (p == null) return null;

            return new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                GithubLink = p.GithubLink,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt
            };
        }

        public async Task<int> CreateAsync(CreateProjectDto dto)
        {
            var project = new Project
            {
                Title = dto.Title,
                Description = dto.Description,
                GithubLink = dto.GithubLink,
                ImageUrl = dto.ImageUrl
            };

            dbContext.Projects.Add(project);
            await dbContext.SaveChangesAsync();

            return project.Id;
        }

        public async Task<bool> UpdateAsync(int id, UpdateProjectDto dto)
        {
            var project = await dbContext.Projects.FindAsync(id);
            if (project == null) return false;

            project.Title = dto.Title;
            project.Description = dto.Description;
            project.GithubLink = dto.GithubLink;
            project.ImageUrl = dto.ImageUrl;

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await dbContext.Projects.FindAsync(id);
            if (project == null) return false;

            dbContext.Projects.Remove(project);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
