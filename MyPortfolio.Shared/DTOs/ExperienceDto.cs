namespace MyPortfolio.Shared.DTOs;

public class ExperienceDto
{
    public string Company { get; set; } = default!;
    public string Position { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}
