using AutoMapper;
using OrayPortfolio.Application.DTOs.Experience;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Mapping
{
    public class ExperienceProfile : AutoMapper.Profile
    {
        public ExperienceProfile()
        {
            CreateMap<Experience, ExperienceUpdateDto>().ReverseMap();

            CreateMap<ExperienceCreateDto, Experience>()
                .ForMember(dest => dest.LogoImageUrl, opt => opt.MapFrom(src => src.LogoImageUrl));

            CreateMap<ExperienceUpdateDto, Experience>()
                .ForMember(dest => dest.LogoImageUrl, opt => opt.MapFrom(src => src.LogoImageUrl));
        }
    }
}
