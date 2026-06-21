using System.ComponentModel.DataAnnotations;

namespace OrayPortfolio.Web.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        [StringLength(60, ErrorMessage = "Ad Soyad en fazla 60 karakter olabilir.")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email formatı girin.")]
        [StringLength(100, ErrorMessage = "Email en fazla 100 karakter olabilir.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Konu zorunludur.")]
        [StringLength(100, ErrorMessage = "Konu en fazla 100 karakter olabilir.")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Mesaj zorunludur.")]
        [StringLength(2000, ErrorMessage = "Mesaj en fazla 2000 karakter olabilir.")]
        public string? Message { get; set; }

        // 📌 SPAM KORUMASI: Bal Küpü (Honeypot) Alanı
        // Gerçek kullanıcı bunu görmez, botlar doldurur. Doldurulursa spamdır!
        public string? WebsiteUrl { get; set; }
    }
}