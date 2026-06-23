using OrayPortfolio.Application.DTOs.Certificate;
using OrayPortfolio.Application.DTOs.Education;
using OrayPortfolio.Application.DTOs.Profile;
using OrayPortfolio.Application.DTOs.Project;
using OrayPortfolio.Application.DTOs.Reference;
using OrayPortfolio.Application.DTOs.VolunteerWork;

namespace OrayPortfolio.Application.DTOs.Dashboard
{
    public class DashboardViewModel
    {
        // Sayısal istatistikler
        public int ProjectCount { get; set; }
        public int ExperienceCount { get; set; }
        public int CertificateCount { get; set; }
        public int SkillCount { get; set; }
        public int VolunteerWorkCount { get; set; }
        public int EducationCount { get; set; }
        public int ReferenceCount { get; set; }

        // Profil tamamlanma
        public int ProfileCompletion { get; set; }

        public int TodayVisitors { get; set; }
        public int TotalVisitors { get; set; }
        public List<string> WeeklyVisitorDates { get; set; } = new();
        public List<int> WeeklyVisitorCounts { get; set; } = new();

        // Son eklenenler
        public List<ProjectDto>? LastProjects { get; set; }
        public List<CertificateUpdateDto>? LastCertificates { get; set; }
        public List<VolunteerWorkUpdateDto>? LastVolunteerWorks { get; set; }
        public List<EducationDto>? LastEducations { get; set; }
        public List<ReferenceDto>? LastReferences { get; set; }
    }
}
