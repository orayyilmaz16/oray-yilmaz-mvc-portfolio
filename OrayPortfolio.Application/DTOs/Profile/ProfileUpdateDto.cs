

namespace OrayPortfolio.Application.DTOs.Profile
{
    public class ProfileUpdateDto
    {
        public int Id { get; set; }

        public string? FullName { get; set; }
        public string? Title { get; set; }
        public string? ShortBio { get; set; }
        public string? LongBio { get; set; }

        public string? GithubUrl { get; set; }
        public string? LinkedinUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? Email { get; set; }

        public string? CvFilePath { get; set; }
        public string? ProfileImageUrl { get; set; } // Admin panelde gösterim için
    }
}
