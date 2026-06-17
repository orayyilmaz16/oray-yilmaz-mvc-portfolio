using AutoMapper;
using OrayPortfolio.Application.DTOs.Skill;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Mapping
{
    public class SkillProfile : AutoMapper.Profile
    {
        public SkillProfile()
        {
            CreateMap<Skill, SkillDto>().ReverseMap();
        }
    }
}
