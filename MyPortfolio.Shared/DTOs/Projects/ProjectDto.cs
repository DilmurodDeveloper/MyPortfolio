namespace MyPortfolio.Shared.DTOs.Projects;

public class ProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? GithubLink { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
