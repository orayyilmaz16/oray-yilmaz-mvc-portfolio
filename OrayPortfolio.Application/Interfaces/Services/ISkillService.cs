using OrayPortfolio.Application.DTOs.Skill;

public interface ISkillService
{
    Task<List<SkillDto>> GetAllAsync();
    Task<bool> CreateAsync(SkillDto dto);
    Task<bool> UpdateAsync(SkillDto dto);
    Task<bool> DeleteAsync(int id);
    Task<SkillDto?> GetByIdAsync(int id);
}
