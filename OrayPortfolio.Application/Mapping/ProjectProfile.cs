using AutoMapper;
using OrayPortfolio.Application.DTOs.Project;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Mapping
{
    public class ProjectProfile : AutoMapper.Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectUpdateDto>().ReverseMap();
            CreateMap<ProjectCreateDto, Project>();
        }
    }
}
