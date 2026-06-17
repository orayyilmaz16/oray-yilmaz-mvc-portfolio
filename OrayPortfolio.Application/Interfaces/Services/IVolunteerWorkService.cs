using OrayPortfolio.Application.DTOs.VolunteerWork;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface IVolunteerWorkService
    {
        Task<List<VolunteerWorkDto>> GetAllAsync();
        Task<VolunteerWorkDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(VolunteerWorkCreateDto dto);
        Task<bool> UpdateAsync(VolunteerWorkDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
