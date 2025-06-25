namespace MyPortfolio.Shared.DTOs
{
    public class ProjectRequestDto
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public string? ProjectUrl { get; set; }
    }
}
