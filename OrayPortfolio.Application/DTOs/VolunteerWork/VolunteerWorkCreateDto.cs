namespace OrayPortfolio.Application.DTOs.VolunteerWork
{
    public class VolunteerWorkCreateDto
    {
        public string? Organization { get; set; }
        public string? Role { get; set; }
        public string? Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
