using OrayPortfolio.Domain.Common;

namespace OrayPortfolio.Domain.Entities
{
    public class Reference : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Position { get; set; }
        public string? Company { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Comment { get; set; }

        public string? ProfileImageUrl { get; set; } // İsteğe bağlı profil fotosu
        public string? LinkedinUrl { get; set; }     // İsteğe bağlı LinkedIn linki
    }
}
