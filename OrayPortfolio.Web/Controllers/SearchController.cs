using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Models;

namespace OrayPortfolio.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IExperienceService _experienceService;
        private readonly ISkillService _skillService;
        private readonly ICertificateService _certificateService;
        private readonly IEducationService _educationService;
        private readonly IReferenceService _referenceService;
        private readonly IVolunteerWorkService _volunteerWorkService;

        public SearchController(
            IProjectService projectService,
            IExperienceService experienceService,
            ISkillService skillService,
            ICertificateService certificateService,
            IEducationService educationService,
            IReferenceService referenceService,
            IVolunteerWorkService volunteerWorkService)
        {
            _projectService = projectService;
            _experienceService = experienceService;
            _skillService = skillService;
            _certificateService = certificateService;
            _educationService = educationService;
            _referenceService = referenceService;
            _volunteerWorkService = volunteerWorkService;
        }

        public async Task<IActionResult> Index(string q)
        {
            var vm = new SearchViewModel
            {
                Query = q ?? ""
            };

            if (string.IsNullOrWhiteSpace(q))
                return View(vm);

            // Tüm verileri çek
            var projects = await _projectService.GetAllAsync();
            var experiences = await _experienceService.GetAllAsync();
            var skills = await _skillService.GetAllAsync();
            var certificates = await _certificateService.GetAllAsync();
            var educations = await _educationService.GetAllAsync();
            var references = await _referenceService.GetAllAsync();
            var volunteerWorks = await _volunteerWorkService.GetAllAsync();

            // Filtreleme
            vm.Projects = projects
                .Where(x => x.Title.Contains(q, StringComparison.OrdinalIgnoreCase))
                .ToList();

            vm.Experiences = experiences
                .Where(x => x.Position.Contains(q, StringComparison.OrdinalIgnoreCase))
                .ToList();

            vm.Skills = skills
                .Where(x => x.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
                .ToList();

            vm.Certificates = certificates
                .Where(x => x.Title.Contains(q, StringComparison.OrdinalIgnoreCase))
                .ToList();

            vm.Educations = educations
                .Where(x => x.School.Contains(q, StringComparison.OrdinalIgnoreCase)
                         || x.Department.Contains(q, StringComparison.OrdinalIgnoreCase))
                .ToList();

            vm.References = references
                .Where(x => x.FullName.Contains(q, StringComparison.OrdinalIgnoreCase)
                         || x.Position.Contains(q, StringComparison.OrdinalIgnoreCase))
                .ToList();

            vm.VolunteerWorks = volunteerWorks
                .Where(x => x.Role.Contains(q, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return View(vm);
        }
    }
}
