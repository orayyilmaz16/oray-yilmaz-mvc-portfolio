using AutoMapper;
using OrayPortfolio.Application.DTOs.VolunteerWork;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Mapping
{
    public class VolunteerWorkProfile : AutoMapper.Profile
    {
        public VolunteerWorkProfile()
        {
            CreateMap<VolunteerWork, VolunteerWorkDto>().ReverseMap();
            CreateMap<VolunteerWorkCreateDto, VolunteerWork>();
        }
    }
}
