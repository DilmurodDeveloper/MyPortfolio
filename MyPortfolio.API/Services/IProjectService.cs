using MyPortfolio.API.Models;

namespace MyPortfolio.API.Services
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
        Project? GetById(int id);
    }
}