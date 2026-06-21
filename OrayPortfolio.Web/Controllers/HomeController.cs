using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Models;
using System.Net;
using System.Net.Mail;

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
        private readonly IConfiguration _config; // 📌 EKLENDİ

        public HomeController(
            IProfileService profileService,
            IProjectService projectService,
            IExperienceService experienceService,
            IEducationService educationService,
            ISkillService skillService,
            IReferenceService referenceService,
            IVolunteerWorkService volunteerWorkService,
            ICertificateService certificateService,
            IConfiguration config) // 📌 EKLENDİ
        {
            _profileService = profileService;
            _projectService = projectService;
            _experienceService = experienceService;
            _educationService = educationService;
            _skillService = skillService;
            _referenceService = referenceService;
            _volunteerWorkService = volunteerWorkService;
            _certificateService = certificateService;
            _config = config; // 📌 EKLENDİ
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel
            {
                Profile = await _profileService.GetAsync(),
                FeaturedProjects = (await _projectService.GetAllAsync())
                    .Where(p => p.IsFeatured)
                    .OrderByDescending(p => p.Id)
                    .ToList(),
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

        [HttpPost]
        public IActionResult SendMessage([FromBody] ContactViewModel ContactForm)
        {
            // 📌 SPAM KONTROLÜ (Honeypot) - Bot tuzağa düşerse "başarılı" dönüp kandırıyoruz
            if (!string.IsNullOrEmpty(ContactForm.WebsiteUrl))
                return Json(new { success = true, message = "Mesajınız başarıyla gönderildi!" });

            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Lütfen tüm alanları doğru şekilde doldurun." });

            try
            {
                // SMTP bilgilerini appsettings.json'dan alıyoruz
                var smtpEmail = _config["Smtp:Email"];
                var smtpPassword = _config["Smtp:Password"];

                var mail = new MailMessage();
                mail.From = new MailAddress(smtpEmail, ContactForm.FullName);
                mail.To.Add(smtpEmail);
                mail.Subject = ContactForm.Subject;
                mail.Body =
                    $"Gönderen: {ContactForm.FullName}\n" +
                    $"Email: {ContactForm.Email}\n\n" +
                    $"Mesaj:\n{ContactForm.Message}";
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
