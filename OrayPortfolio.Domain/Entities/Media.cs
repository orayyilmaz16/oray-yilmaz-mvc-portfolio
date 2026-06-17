using OrayPortfolio.Domain.Common;

namespace OrayPortfolio.Domain.Entities
{
    public class Media : BaseEntity
    {
        public string? FileName { get; set; }          // orijinal dosya adı
        public string? StoredFileName { get; set; }    // sunucuda saklanan isim (GUID + uzantı)
        public string? FilePath { get; set; }          // /uploads/experience/abc123.png
        public string? FileType { get; set; }          // image, pdf, video, doc, etc.
        public string? Caption { get; set; }

        // İlişkiler
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }

        public int? ExperienceId { get; set; }
        public Experience? Experience { get; set; }

        public int? CertificateId { get; set; }
        public Certificate? Certificate { get; set; }

        public int? VolunteerWorkId { get; set; }
        public VolunteerWork? VolunteerWork { get; set; }
    }
}
