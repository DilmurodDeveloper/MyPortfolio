namespace MyPortfolio.Shared.DTOs
{
    public class BlogPostRequestDto
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string? ImageUrl { get; set; }
    }
}
