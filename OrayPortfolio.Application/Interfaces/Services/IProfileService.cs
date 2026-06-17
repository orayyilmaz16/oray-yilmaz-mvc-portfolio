using OrayPortfolio.Application.DTOs.Profile;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface IProfileService
    {
        Task<ProfileDto> GetAsync();
        Task<bool> UpdateAsync(ProfileUpdateDto dto);


    }
}
