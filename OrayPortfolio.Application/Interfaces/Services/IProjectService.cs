using OrayPortfolio.Application.DTOs.Project;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAllAsync();
        Task<ProjectUpdateDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(ProjectCreateDto dto);
        Task<bool> UpdateAsync(ProjectUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
