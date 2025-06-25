using MyPortfolio.API.Models;

namespace MyPortfolio.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly List<Project> projects = new()
    {
        new Project
        {
            Id = 1,
            Title = "My Portfolio Website",
            Description = "A personal portfolio built with Blazor WebAssembly.",
            ImageUrl = "/images/portfolio.png",
            ProjectUrl = "https://myportfolio.com",
            CreatedAt = DateTimeOffset.UtcNow.AddMonths(-1)
        },
        new Project
        {
            Id = 2,
            Title = "Todo App",
            Description = "A simple task management app.",
            ImageUrl = "/images/todo.png",
            ProjectUrl = "https://github.com/me/todoapp",
            CreatedAt = DateTimeOffset.UtcNow.AddMonths(-2)
        }
    };

        public IEnumerable<Project> GetAll() => projects;

        public Project? GetById(int id) => projects.FirstOrDefault(p => p.Id == id);
    }
}
