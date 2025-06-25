namespace MyPortfolio.Shared.DTOs
{
    public class ContactMessageDto : ContactRequestDto
    {
        public int Id { get; set; }
        public DateTimeOffset SentAt { get; set; }
    }
}
