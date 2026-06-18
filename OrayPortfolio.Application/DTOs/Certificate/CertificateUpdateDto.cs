namespace OrayPortfolio.Application.DTOs.Certificate
{
    public class CertificateUpdateDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Issuer { get; set; }
        public DateTime? Date { get; set; }
        public string? CertificateUrl { get; set; }

        public string? FileUrl { get; set; }

    }
}
