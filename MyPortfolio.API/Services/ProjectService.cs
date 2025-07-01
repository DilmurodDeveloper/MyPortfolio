using MyPortfolio.API.Models;

namespace MyPortfolio.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly List<Project> projects = new();

        public IEnumerable<Project> GetAll() => projects;

        public Project? GetById(int id) =>
            projects.FirstOrDefault(p => p.Id == id);

        public void Add(Project project)
        {
            project.Id = projects.Count + 1;
            project.CreatedAt = DateTimeOffset.UtcNow;
            projects.Add(project);
        }

        public void Update(Project project)
        {
            var existing = GetById(project.Id);
            if (existing == null) return;

            existing.Title = project.Title;
            existing.Description = project.Description;
            existing.ImageUrl = project.ImageUrl;
            existing.ProjectUrl = project.ProjectUrl;
        }

        public void Delete(int id)
        {
            var project = GetById(id);
            if (project != null) projects.Remove(project);
        }
    }
}
