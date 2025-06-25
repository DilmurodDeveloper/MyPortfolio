using MyPortfolio.API.Models;

namespace MyPortfolio.API.Services
{
    public class ContactService : IContactService
    {
        private readonly List<ContactMessage> messages = new();

        public IEnumerable<ContactMessage> GetAll() => messages;

        public ContactMessage? GetById(int id) => 
            messages.FirstOrDefault(m => m.Id == id);

        public void Add(ContactMessage message)
        {
            message.Id = messages.Count > 0 ? 
                messages.Max(m => m.Id) + 1 : 1;

            message.SentAt = DateTime.UtcNow;
            messages.Add(message);
        }
    }
}
