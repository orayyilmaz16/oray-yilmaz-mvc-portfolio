using OrayPortfolio.Domain.Common;

namespace OrayPortfolio.Domain.Entities
{
    public class VolunteerWork
    {
        public int Id { get; set; }
        public string? Organization { get; set; }
        public string? Role { get; set; }
        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }

        public string? ImageUrl { get; set; }
    }

}
