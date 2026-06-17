using OrayPortfolio.Application.DTOs.VolunteerWork;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface IVolunteerWorkService
    {
        Task<List<VolunteerWorkUpdateDto>> GetAllAsync();
        Task<VolunteerWorkUpdateDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(VolunteerWorkCreateDto dto);
        Task<bool> UpdateAsync(VolunteerWorkUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
