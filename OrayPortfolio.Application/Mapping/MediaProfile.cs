using AutoMapper;
using OrayPortfolio.Application.DTOs.Media;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Mapping
{
    public class MediaProfile : AutoMapper.Profile
    {
        public MediaProfile()
        {
            CreateMap<Media, MediaDto>().ReverseMap();
            CreateMap<Media, MediaCreateDto>().ReverseMap();
        }
    }
}
