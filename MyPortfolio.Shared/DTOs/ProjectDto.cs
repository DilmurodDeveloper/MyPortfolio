namespace MyPortfolio.Shared.DTOs;

public class ProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public string? ProjectUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
