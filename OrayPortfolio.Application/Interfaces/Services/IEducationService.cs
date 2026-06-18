using OrayPortfolio.Application.DTOs.Education;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface IEducationService
    {
        Task<List<EducationDto>> GetAllAsync();
        Task<EducationDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(EducationCreateDto dto);
        Task<bool> UpdateAsync(EducationDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
