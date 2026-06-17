using OrayPortfolio.Application.DTOs.Experience;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface IExperienceService
    {
        Task<List<ExperienceUpdateDto>> GetAllAsync();
        Task<ExperienceUpdateDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(ExperienceCreateDto dto);
        Task<bool> UpdateAsync(ExperienceUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
