using Microsoft.EntityFrameworkCore;
using MyPortfolio.Shared.DTOs.ContactMessages;
using MyPortfolio.Shared.Models;

namespace MyPortfolio.API.Services.ContactMessages
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly AppDbContext db;

        public ContactMessageService(AppDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task<List<ContactMessageDto>> GetAllAsync()
        {
            return await db.ContactMessages
                .OrderByDescending(m => m.CreatedAt)
                .Select(m => new ContactMessageDto
                {
                    Id = m.Id,
                    FullName = m.FullName,
                    Email = m.Email,
                    Message = m.Message,
                    CreatedAt = m.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<int> CreateAsync(CreateContactMessageDto dto)
        {
            var message = new ContactMessage
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Message = dto.Message
            };

            db.ContactMessages.Add(message);
            await db.SaveChangesAsync();
            return message.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await db.ContactMessages.FindAsync(id);
            if (entity is null) return false;

            db.ContactMessages.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
