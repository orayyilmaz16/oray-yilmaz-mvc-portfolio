using AutoMapper;
using OrayPortfolio.Application.DTOs.Media;
using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Services
{
    public class MediaService : IMediaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MediaService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<MediaDto> CreateMediaRecordAsync(Media media)
        {
            await _uow.MediaFiles.AddAsync(media);
            await _uow.SaveAsync();

            return _mapper.Map<MediaDto>(media);
        }
    }
}
