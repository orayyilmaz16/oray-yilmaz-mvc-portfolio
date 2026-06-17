namespace OrayPortfolio.Application.DTOs.Experience
{
    public class ExperienceCreateDto
    {
        public string? Company { get; set; }
        public string? Position { get; set; }
        public string? Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
    }
}
