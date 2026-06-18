using OrayPortfolio.Application.DTOs.Skill;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface ISkillService
    {
        Task<List<SkillUpdateDto>> GetAllAsync();
        Task<SkillUpdateDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(SkillCreateDto dto);
        Task<bool> UpdateAsync(SkillUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}