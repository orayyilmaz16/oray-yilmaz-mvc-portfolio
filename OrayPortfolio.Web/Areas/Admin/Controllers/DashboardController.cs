using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Dashboard;
using OrayPortfolio.Application.DTOs.Profile;
using OrayPortfolio.Application.Interfaces.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : BaseAdminController
    {
        private readonly IProjectService _projectService;
        private readonly IExperienceService _experienceService;
        private readonly ICertificateService _certificateService;
        private readonly ISkillService _skillService;
        private readonly IVolunteerWorkService _volunteerService;
        private readonly IProfileService _profileService;
        private readonly IEducationService _educationService;
        private readonly IReferenceService _referenceService;
        private readonly IVisitorService _visitorService;

        public DashboardController(
            IProjectService projectService,
            IReferenceService referenceService,
            IExperienceService experienceService,
            ICertificateService certificateService,
            ISkillService skillService,
            IVolunteerWorkService volunteerService,
            IProfileService profileService,
            IEducationService educationService,
            IVisitorService visitorService)
        {
            _projectService = projectService;
            _experienceService = experienceService;
            _referenceService = referenceService;
            _certificateService = certificateService;
            _skillService = skillService;
            _volunteerService = volunteerService;
            _profileService = profileService;
            _educationService = educationService;
            _visitorService = visitorService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAllAsync();
            var experiences = await _experienceService.GetAllAsync();
            var certificates = await _certificateService.GetAllAsync();
            var skills = await _skillService.GetAllAsync();
            var volunteers = await _volunteerService.GetAllAsync();
            var references = await _referenceService.GetAllAsync();
            var educations = await _educationService.GetAllAsync();
            var profile = await _profileService.GetAsync();
            var visitorStats = await _visitorService.GetVisitorStatsAsync();

            var model = new DashboardViewModel
            {
                ProjectCount = projects.Count,
                ExperienceCount = experiences.Count,
                CertificateCount = certificates.Count,
                SkillCount = skills.Count,
                VolunteerWorkCount = volunteers.Count,
                ReferenceCount = references.Count,
                EducationCount = educations.Count,

                ProfileCompletion = CalculateProfileCompletion(profile),

                LastProjects = projects.OrderByDescending(p => p.Id).Take(5).ToList(),
                LastCertificates = certificates.OrderByDescending(c => c.Id).Take(5).ToList(),
                LastVolunteerWorks = volunteers.OrderByDescending(v => v.Id).Take(5).ToList(),
                LastEducations = educations.OrderByDescending(e => e.Id).Take(5).ToList(),
                LastReferences = references.OrderByDescending(r => r.Id).Take(5).ToList(),

                // 📌 SORUN BURADAYDI: Deneyim ve Yetenek listelerini Modele aktarmayı atlamıştık, eklendi!
                LastExperiences = experiences.OrderByDescending(x => x.Id).Take(5).ToList(),
                LastSkills = skills.OrderByDescending(s => s.Id).Take(5).ToList(),

                TodayVisitors = visitorStats.TodayVisitors,
                TotalVisitors = visitorStats.TotalVisitors,
                WeeklyVisitorDates = visitorStats.WeeklyVisitorDates,
                WeeklyVisitorCounts = visitorStats.WeeklyVisitorCounts
            };

            return View(model);
        }

        [HttpGet("/Admin/Dashboard/GetDashboardDataJson")]
        public async Task<IActionResult> GetDashboardDataJson()
        {
            var projects = await _projectService.GetAllAsync();
            var experiences = await _experienceService.GetAllAsync();
            var certificates = await _certificateService.GetAllAsync();
            var skills = await _skillService.GetAllAsync();
            var volunteers = await _volunteerService.GetAllAsync();
            var references = await _referenceService.GetAllAsync();
            var educations = await _educationService.GetAllAsync();
            var profile = await _profileService.GetAsync();
            var visitorStats = await _visitorService.GetVisitorStatsAsync();

            var data = new
            {
                projectCount = projects.Count,
                experienceCount = experiences.Count,
                certificateCount = certificates.Count,
                skillCount = skills.Count,
                volunteerWorkCount = volunteers.Count,
                referenceCount = references.Count,
                educationCount = educations.Count,
                profileCompletion = CalculateProfileCompletion(profile),
                todayVisitors = visitorStats.TodayVisitors,
                totalVisitors = visitorStats.TotalVisitors,
                weeklyVisitorDates = visitorStats.WeeklyVisitorDates,
                weeklyVisitorCounts = visitorStats.WeeklyVisitorCounts,

                // 📌 YENİLE BUTONU İÇİN LİSTELER (AJAX için JSON formatında)
                lastProjects = projects.OrderByDescending(p => p.Id).Take(5).Select(p => new { text = p.Title }).ToList(),
                lastEducations = educations.OrderByDescending(e => e.Id).Take(5).Select(e => new { text = e.School }).ToList(),
                lastReferences = references.OrderByDescending(r => r.Id).Take(5).Select(r => new { text = r.FullName }).ToList(),
                lastCertificates = certificates.OrderByDescending(c => c.Id).Take(5).Select(c => new { text = c.Title }).ToList(),

                // Deneyim ve Yeteneklerin JSON karşılıkları eklendi
                lastExperiences = experiences.OrderByDescending(x => x.Id).Take(5).Select(x => new { text = x.Position }).ToList(),
                lastSkills = skills.OrderByDescending(s => s.Id).Take(5).Select(s => new { text = s.Name }).ToList(),

                lastVolunteerWorks = volunteers.OrderByDescending(v => v.Id).Take(5).Select(v => new { text = v.Organization }).ToList()
            };

            return Json(data);
        }

        private int CalculateProfileCompletion(ProfileDto profile)
        {
            if (profile == null) return 0;
            var fields = new[] { profile.FullName, profile.Title, profile.ShortBio, profile.LongBio, profile.Email, profile.GithubUrl, profile.LinkedinUrl, profile.ProfileImageUrl };
            int filled = fields.Count(f => !string.IsNullOrWhiteSpace(f));
            return (int)((double)filled / fields.Length * 100);
        }
    }
}