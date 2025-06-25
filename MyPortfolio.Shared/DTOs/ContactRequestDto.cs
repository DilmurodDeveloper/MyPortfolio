namespace MyPortfolio.Shared.DTOs;

public class ContactRequestDto
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Message { get; set; } = default!;
}
