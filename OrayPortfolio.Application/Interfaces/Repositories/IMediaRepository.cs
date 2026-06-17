using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Interfaces.Repositories
{
    public interface IMediaRepository : IGenericRepository<Media>
    {
        Task<List<Media>> GetByProjectIdAsync(int projectId);
        Task<List<Media>> GetByExperienceIdAsync(int experienceId);
        Task<List<Media>> GetByCertificateIdAsync(int certificateId);
        Task<List<Media>> GetByVolunteerWorkIdAsync(int volunteerWorkId);
    }
}
