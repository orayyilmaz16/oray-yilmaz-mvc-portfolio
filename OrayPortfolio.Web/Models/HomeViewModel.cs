using OrayPortfolio.Application.DTOs.Certificate;
using OrayPortfolio.Application.DTOs.Education;
using OrayPortfolio.Application.DTOs.Experience;
using OrayPortfolio.Application.DTOs.Profile;
using OrayPortfolio.Application.DTOs.Project;
using OrayPortfolio.Application.DTOs.Reference;
using OrayPortfolio.Application.DTOs.Skill;
using OrayPortfolio.Application.DTOs.VolunteerWork;

namespace OrayPortfolio.Web.Models
{
    public class HomeViewModel
    {
        public ProfileDto? Profile { get; set; }

        public List<ProjectDto>? FeaturedProjects { get; set; }
        public List<ExperienceUpdateDto>? LatestExperiences { get; set; }

        public ContactViewModel ContactForm { get; set; } = new ContactViewModel();

        public List<EducationDto>? Educations { get; set; }
        public List<SkillUpdateDto>? Skills { get; set; }
        public List<ReferenceDto>? References { get; set; }

        public List<VolunteerWorkUpdateDto>? VolunteerWorks { get; set; }
        public List<CertificateUpdateDto>? Certificates { get; set; }
    }
}