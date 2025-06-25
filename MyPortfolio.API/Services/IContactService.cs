using MyPortfolio.API.Models;

namespace MyPortfolio.API.Services
{
    public interface IContactService
    {
        IEnumerable<ContactMessage> GetAll();
        ContactMessage? GetById(int id);
        void Add(ContactMessage message);
    }
}
