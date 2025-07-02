namespace MyPortfolio.Shared.DTOs.ContactMessages
{
    public class CreateContactMessageDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}