using AutoMapper;
using OrayPortfolio.Application.DTOs.Project;
using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ProjectUpdateDto>> GetAllAsync()
        {
            var data = await _uow.Projects.GetAllAsync();
            return _mapper.Map<List<ProjectUpdateDto>>(data);
        }

        public async Task<ProjectUpdateDto> GetByIdAsync(int id)
        {
            var entity = await _uow.Projects.GetByIdAsync(id);
            return _mapper.Map<ProjectUpdateDto>(entity);
        }

        public async Task<bool> CreateAsync(ProjectCreateDto dto)
        {
            var entity = _mapper.Map<Project>(dto);
            await _uow.Projects.AddAsync(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ProjectUpdateDto dto)
        {
            var entity = _mapper.Map<Project>(dto);
            _uow.Projects.Update(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Projects.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Projects.Delete(entity);
            return await _uow.SaveAsync() > 0;
        }
    }
}
