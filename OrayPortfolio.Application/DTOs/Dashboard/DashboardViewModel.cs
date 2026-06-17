using OrayPortfolio.Application.DTOs.Certificate;
using OrayPortfolio.Application.DTOs.Profile;
using OrayPortfolio.Application.DTOs.Project;

namespace OrayPortfolio.Application.DTOs.Dashboard
{
    public class DashboardViewModel
    {
        public int ProjectCount { get; set; }
        public int ExperienceCount { get; set; }
        public int CertificateCount { get; set; }
        public int SkillCount { get; set; }
        public int VolunteerWorkCount { get; set; }

        public int ProfileCompletion { get; set; }

        public ProfileDto Profile { get; set; }

        public List<ProjectUpdateDto> LastProjects { get; set; } = new();
        public List<CertificateUpdateDto> LastCertificates { get; set; } = new();
    }
}
