using MyPortfolio.API.Models;

namespace MyPortfolio.API.Services
{
    public interface IBlogService
    {
        IEnumerable<BlogPost> GetAll();
        BlogPost? GetById(int id);
    }
}
