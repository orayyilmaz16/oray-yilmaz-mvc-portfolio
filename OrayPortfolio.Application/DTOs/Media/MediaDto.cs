namespace OrayPortfolio.Application.DTOs.Media
{
    public class MediaDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string StoredFileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;

        public int? ProjectId { get; set; }
        public int? ExperienceId { get; set; }
        public int? CertificateId { get; set; }
        public int? VolunteerWorkId { get; set; }
    }
}
