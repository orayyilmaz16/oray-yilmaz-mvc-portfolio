using OrayPortfolio.Application.DTOs.Project;
using OrayPortfolio.Application.DTOs.Experience;
using OrayPortfolio.Application.DTOs.Skill;
using OrayPortfolio.Application.DTOs.Certificate;
using OrayPortfolio.Application.DTOs.Education;
using OrayPortfolio.Application.DTOs.Reference;
using OrayPortfolio.Application.DTOs.VolunteerWork;

namespace OrayPortfolio.Web.Models
{
    public class SearchViewModel
    {
        public string Query { get; set; }

        public List<ProjectDto> Projects { get; set; } = new();
        public List<ExperienceUpdateDto> Experiences { get; set; } = new();
        public List<SkillUpdateDto> Skills { get; set; } = new();
        public List<CertificateUpdateDto> Certificates { get; set; } = new();
        public List<EducationDto> Educations { get; set; } = new();
        public List<ReferenceDto> References { get; set; } = new();
        public List<VolunteerWorkUpdateDto> VolunteerWorks { get; set; } = new();
    }
}
