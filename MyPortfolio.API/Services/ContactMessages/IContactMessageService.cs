using MyPortfolio.Shared.DTOs.ContactMessages;

namespace MyPortfolio.API.Services.ContactMessages
{
    public interface IContactMessageService
    {
        Task<List<ContactMessageDto>> GetAllAsync();
        Task<int> CreateAsync(CreateContactMessageDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
