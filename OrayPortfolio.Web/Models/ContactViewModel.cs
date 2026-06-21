using System.ComponentModel.DataAnnotations;

namespace OrayPortfolio.Web.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Ad Soyad zorunludur.")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email girin.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Konu zorunludur.")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Mesaj zorunludur.")]
        public string? Message { get; set; }
    }
}