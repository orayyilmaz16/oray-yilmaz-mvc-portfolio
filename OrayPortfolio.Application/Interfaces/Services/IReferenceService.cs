using OrayPortfolio.Application.DTOs.Reference;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface IReferenceService
    {
        Task<List<ReferenceDto>> GetAllAsync();
        Task<ReferenceDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReferenceCreateDto dto);
        Task<bool> UpdateAsync(ReferenceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
