using MyPortfolio.API.Models;

namespace MyPortfolio.API.Services
{
    public interface IBlogService
    {
        IEnumerable<BlogPost> GetAll();
        BlogPost? GetById(int id);
        void Add(BlogPost post);
        void Update(BlogPost post);
        void Delete(int id);
    }
}
