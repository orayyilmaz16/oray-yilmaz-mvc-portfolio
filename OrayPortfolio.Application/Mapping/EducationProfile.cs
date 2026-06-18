using OrayPortfolio.Application.DTOs.Education;
using OrayPortfolio.Domain.Entities;
using AutoMapper;

namespace OrayPortfolio.Application.Mapping
{
    public class EducationProfile : AutoMapper.Profile
    {
        public EducationProfile()
        {
            CreateMap<Education, EducationDto>().ReverseMap();
            CreateMap<Education, EducationCreateDto>().ReverseMap();
        }
    }
}
