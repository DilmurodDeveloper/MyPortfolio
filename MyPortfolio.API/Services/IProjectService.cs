using MyPortfolio.API.Models;

namespace MyPortfolio.API.Services
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
        Project? GetById(int id);
        void Add(Project project);
        void Update(Project project);
        void Delete(int id);
    }
}