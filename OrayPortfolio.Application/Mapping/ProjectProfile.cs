using AutoMapper;
using OrayPortfolio.Application.DTOs.Project;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Mapping
{
    public class ProjectProfile : AutoMapper.Profile
    {
        public ProjectProfile()
        {
            // CREATE
            CreateMap<Project, ProjectCreateDto>();
            CreateMap<ProjectCreateDto, Project>();

            // UPDATE
            CreateMap<Project, ProjectUpdateDto>();
            CreateMap<ProjectUpdateDto, Project>();

            // LISTING (Index)
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
        }
    }
}
