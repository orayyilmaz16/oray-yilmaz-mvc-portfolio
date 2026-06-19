using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Models;

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

        public HomeController(
            IProfileService profileService,
            IProjectService projectService,
            IExperienceService experienceService,
            IEducationService educationService,
            ISkillService skillService,
            IReferenceService referenceService,
            IVolunteerWorkService volunteerWorkService,
            ICertificateService certificateService)
        {
            _profileService = profileService;
            _projectService = projectService;
            _experienceService = experienceService;
            _educationService = educationService;
            _skillService = skillService;
            _referenceService = referenceService;
            _volunteerWorkService = volunteerWorkService;
            _certificateService = certificateService;
        }

        public async Task<IActionResult> Index()
        {
            // Tüm verileri çek
            var allProjects = await _projectService.GetAllAsync();
            var allExperiences = await _experienceService.GetAllAsync();
            var allEducations = await _educationService.GetAllAsync();
            var allSkills = await _skillService.GetAllAsync();
            var allReferences = await _referenceService.GetAllAsync();
            var allVolunteerWorks = await _volunteerWorkService.GetAllAsync();
            var allCertificates = await _certificateService.GetAllAsync();

            // Öne çıkan projeler
            var featuredProjects = allProjects
                .Where(p => p.IsFeatured)
                .OrderByDescending(p => p.Id)
                .ToList();

            // Son 3 deneyim
            var latestExperiences = allExperiences
                .OrderByDescending(e => e.StartDate)
                .Take(3)
                .ToList();

            var vm = new HomeViewModel
            {
                Profile = await _profileService.GetAsync(),

                FeaturedProjects = featuredProjects,
                LatestExperiences = latestExperiences,

                Educations = allEducations,
                Skills = allSkills,
                References = allReferences,

                VolunteerWorks = allVolunteerWorks,
                Certificates = allCertificates
            };

            return View(vm);
        }
    }
}
