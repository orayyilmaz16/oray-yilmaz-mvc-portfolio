using AutoMapper;
using OrayPortfolio.Application.DTOs.Profile;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Mapping
{
    public class ProfileProfile : AutoMapper.Profile
    {
        public ProfileProfile()
        {
            CreateMap<OrayPortfolio.Domain.Entities.Profile, ProfileDto>();
            CreateMap<OrayPortfolio.Domain.Entities.Profile, ProfileUpdateDto>().ReverseMap();
        }
    }
}
