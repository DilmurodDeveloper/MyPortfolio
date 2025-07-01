namespace MyPortfolio.Shared.Models
{
    public class ExperienceModel
    {
        public string Company { get; }
        public string Role { get; }
        public string Duration { get; }
        public string Location { get; }
        public bool IsLeft { get; }

        public ExperienceModel(string company, string role, string duration, string location, bool isLeft)
        {
            Company = company;
            Role = role;
            Duration = duration;
            Location = location;
            IsLeft = isLeft;
        }
    }
}
