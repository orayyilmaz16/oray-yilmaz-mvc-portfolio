using OrayPortfolio.Domain.Common;

namespace OrayPortfolio.Domain.Entities
{
    public class Education : BaseEntity
    {
        public string? School { get; set; }
        public string? Degree { get; set; }
        public string? Department { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? Description { get; set; }

        public string? LogoImageUrl { get; set; } // İsteğe bağlı kurum logosu
    }
}
