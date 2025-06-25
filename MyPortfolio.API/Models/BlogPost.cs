namespace MyPortfolio.API.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTimeOffset PublishedAt { get; set; }
        public string? ImageUrl { get; set; }
    }
}
