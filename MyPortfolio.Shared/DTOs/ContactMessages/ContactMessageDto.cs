namespace MyPortfolio.Shared.DTOs.ContactMessages
{
    public class ContactMessageDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
    }
}
