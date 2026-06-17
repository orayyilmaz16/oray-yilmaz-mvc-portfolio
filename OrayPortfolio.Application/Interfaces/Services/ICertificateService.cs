using OrayPortfolio.Application.DTOs.Certificate;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface ICertificateService
    {
        Task<List<CertificateUpdateDto>> GetAllAsync();
        Task<CertificateUpdateDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CertificateCreateDto dto);
        Task<bool> UpdateAsync(CertificateUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
