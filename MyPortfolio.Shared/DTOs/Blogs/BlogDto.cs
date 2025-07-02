namespace MyPortfolio.Shared.DTOs.Blogs;

public class BlogDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Content { get; set; }
    public string? ImageUrl { get; set; }
    public DateTimeOffset PublishedAt { get; set; } = DateTimeOffset.UtcNow;
}
