using OrayPortfolio.Application.DTOs.Profile;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface IProfileService
    {
        Task<ProfileUpdateDto> GetAsync();
        Task<bool> UpdateAsync(ProfileUpdateDto dto);
    }
}
