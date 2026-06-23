using System;
using System.Collections.Generic;
using System.Text;

namespace OrayPortfolio.Application.DTOs.Reference
{
    public class ReferenceDto
    {
        public int Id { get; set; }
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
