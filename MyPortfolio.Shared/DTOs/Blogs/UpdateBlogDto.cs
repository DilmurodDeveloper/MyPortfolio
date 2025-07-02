namespace MyPortfolio.Shared.DTOs.Blogs
{
    public class UpdateBlogDto
    {
        public string Title { get; set; } = default!;
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
    }
}
