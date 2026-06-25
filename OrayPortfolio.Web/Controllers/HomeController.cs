using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting; // 📌 YENİ: Rate Limiting için eklendi
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Models;
using System.Net;
using System.Net.Mail;
using Ganss.Xss;

namespace OrayPortfolio.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IProjectService _projectService;
        private readonly IExperienceService _experienceService;
        private readonly IEducationService _educationService;
        private readonly ISkillService _skillService;
        private readonly IReferenceService _referenceService;
        private readonly IVolunteerWorkService _volunteerWorkService;
        private readonly ICertificateService _certificateService;
        private readonly IConfiguration _config;

        public HomeController(
            IProfileService profileService,
            IProjectService projectService,
            IExperienceService experienceService,
            IEducationService educationService,
            ISkillService skillService,
            IReferenceService referenceService,
            IVolunteerWorkService volunteerWorkService,
            ICertificateService certificateService,
            IConfiguration config)
        {
            _profileService = profileService;
            _projectService = projectService;
            _experienceService = experienceService;
            _educationService = educationService;
            _skillService = skillService;
            _referenceService = referenceService;
            _volunteerWorkService = volunteerWorkService;
            _certificateService = certificateService;
            _config = config;
        }

        // 📌 PERFORMANS: Ana sayfa en çok yük çeken yerdir. Saniyeler içinde açılması 
        // ve veritabanını yormaması için 60 saniye boyunca Cache'de (Önbellekte) tutuyoruz.
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> Index()
        {
            var profile = await _profileService.GetAsync();
            var featuredProjects = (await _projectService.GetAllAsync())
                    .Where(p => p.IsFeatured)
                    .OrderByDescending(p => p.Id)
                    .ToList();

            // 📌 GÜVENLİK (XSS): Ziyaretçiye gitmeden önce Zengin Metin alanlarındaki zararlı HTML kodlarını temizliyoruz.
            var sanitizer = new HtmlSanitizer();

            if (profile != null)
            {
                if (!string.IsNullOrEmpty(profile.LongBio))
                    profile.LongBio = sanitizer.Sanitize(profile.LongBio);

                if (!string.IsNullOrEmpty(profile.ShortBio))
                    profile.ShortBio = sanitizer.Sanitize(profile.ShortBio);
            }

            foreach (var proj in featuredProjects)
            {
                if (!string.IsNullOrEmpty(proj.Description))
                    proj.Description = sanitizer.Sanitize(proj.Description);
            }

            var vm = new HomeViewModel
            {
                Profile = profile,
                FeaturedProjects = featuredProjects,
                LatestExperiences = (await _experienceService.GetAllAsync())
                    .OrderByDescending(e => e.StartDate)
                    .Take(3)
                    .ToList(),
                Educations = await _educationService.GetAllAsync(),
                Skills = await _skillService.GetAllAsync(),
                References = await _referenceService.GetAllAsync(),
                VolunteerWorks = await _volunteerWorkService.GetAllAsync(),
                Certificates = await _certificateService.GetAllAsync(),

                ContactForm = new ContactViewModel()
            };

            return View(vm);
        }

        // 📌 GÜVENLİK (CSRF): Başka sitelerden senin formuna gizlice istek atılmasını engeller
        // 📌 GÜVENLİK (Rate Limiting): Dakikada sadece 20 mesaja izin verir. Bot/Spam çökertmesini engeller!
        [HttpPost]
        [ValidateAntiForgeryToken]
        [EnableRateLimiting("fixed")]
        public IActionResult SendMessage([FromBody] ContactViewModel ContactForm)
        {
            // SPAM KONTROLÜ (Honeypot) - Bot tuzağa düşerse "başarılı" dönüp kandırıyoruz
            if (!string.IsNullOrEmpty(ContactForm.WebsiteUrl))
                return Json(new { success = true, message = "Mesajınız başarıyla gönderildi!" });

            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Lütfen tüm alanları doğru şekilde doldurun." });

            try
            {
                // SMTP bilgilerini appsettings.json'dan alıyoruz
                var smtpEmail = _config["Smtp:Email"];
                var smtpPassword = _config["Smtp:Password"];

                // 📌 GÜVENLİK: Gönderilen isim veya mesajın içindeki HTML etiketlerini bile temizliyoruz (Mailde patlamasın diye)
                var sanitizer = new HtmlSanitizer();
                var safeName = sanitizer.Sanitize(ContactForm.FullName);
                var safeMessage = sanitizer.Sanitize(ContactForm.Message);

                var mail = new MailMessage();
                mail.From = new MailAddress(smtpEmail, safeName);
                mail.To.Add(smtpEmail);
                mail.Subject = ContactForm.Subject;
                mail.Body =
                    $"Gönderen: {safeName}\n" +
                    $"Email: {ContactForm.Email}\n\n" +
                    $"Mesaj:\n{safeMessage}";
                mail.IsBodyHtml = false;

                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(smtpEmail, smtpPassword),
                    EnableSsl = true
                };

                smtp.Send(mail);

                return Json(new { success = true, message = "Mesajınız başarıyla gönderildi!" });
            }
            catch
            {
                return Json(new { success = false, message = "Mesaj gönderilirken sunucu tarafında bir hata oluştu." });
            }
        }
    }
}