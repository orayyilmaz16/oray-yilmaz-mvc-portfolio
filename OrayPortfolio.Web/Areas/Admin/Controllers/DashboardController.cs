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

        public DashboardController(
            IProjectService projectService,
            IExperienceService experienceService,
            ICertificateService certificateService,
            ISkillService skillService,
            IVolunteerWorkService volunteerService,
            IProfileService profileService)
        {
            _projectService = projectService;
            _experienceService = experienceService;
            _certificateService = certificateService;
            _skillService = skillService;
            _volunteerService = volunteerService;
            _profileService = profileService;
        }

        public async Task<IActionResult> Index()
        {
            // Tüm sorguları paralel başlat
            var projectsTask = _projectService.GetAllAsync();
            var experiencesTask = _experienceService.GetAllAsync();
            var certificatesTask = _certificateService.GetAllAsync();
            var skillsTask = _skillService.GetAllAsync();
            var volunteersTask = _volunteerService.GetAllAsync();
            var profileTask = _profileService.GetAsync();

            // Hepsini aynı anda bekle
            await Task.WhenAll(
                projectsTask,
                experiencesTask,
                certificatesTask,
                skillsTask,
                volunteersTask,
                profileTask
            );

            // Sonuçları al
            var projects = projectsTask.Result;
            var experiences = experiencesTask.Result;
            var certificates = certificatesTask.Result;
            var skills = skillsTask.Result;
            var volunteers = volunteersTask.Result;
            var profile = profileTask.Result;

            var model = new DashboardViewModel
            {
                ProjectCount = projects.Count,
                ExperienceCount = experiences.Count,
                CertificateCount = certificates.Count,
                SkillCount = skills.Count,
                VolunteerCount = volunteers.Count,
                Profile = profile,
                LastProjects = projects.OrderByDescending(p => p.Id).Take(5).ToList(),
                LastCertificates = certificates.OrderByDescending(c => c.Id).Take(5).ToList(),
                ProfileCompletion = CalculateProfileCompletion(profile)
            };

            return View(model);
        }

        private int CalculateProfileCompletion(ProfileUpdateDto profile)
        {
            if (profile == null)
                return 0;

            var fields = new[]
            {
                profile.FullName,
                profile.Title,
                profile.ShortBio,
                profile.LongBio,
                profile.Email,
                profile.GithubUrl,
                profile.LinkedinUrl,
                profile.ProfileImagePath
            };

            int filled = fields.Count(f => !string.IsNullOrWhiteSpace(f));
            int total = fields.Length;

            return (int)((double)filled / total * 100);
        }
    }
}
