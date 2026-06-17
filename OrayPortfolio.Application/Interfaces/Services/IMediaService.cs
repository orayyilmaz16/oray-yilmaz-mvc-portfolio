using OrayPortfolio.Application.DTOs.Media;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface IMediaService
    {
        Task<MediaDto> CreateMediaRecordAsync(Media media);
    }
}
