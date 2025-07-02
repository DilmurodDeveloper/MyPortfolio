namespace MyPortfolio.Shared.DTOs.Projects
{
    public class CreateProjectDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? GithubLink { get; set; }
    }
}
