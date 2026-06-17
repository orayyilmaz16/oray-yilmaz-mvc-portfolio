using OrayPortfolio.Domain.Common;

namespace OrayPortfolio.Domain.Entities
{
    public class Certificate : BaseEntity
    {
        public string? Title { get; set; }
        public string? Issuer { get; set; }
        public DateTime Date { get; set; }
        public string? CertificateUrl { get; set; }

        public ICollection<Media>? MediaFiles { get; set; }
    }
}
