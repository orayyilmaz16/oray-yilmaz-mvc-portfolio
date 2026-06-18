using AutoMapper;
using OrayPortfolio.Application.DTOs.Education;
using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Services
{
    public class EducationService : IEducationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EducationService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<EducationDto>> GetAllAsync()
        {
            var data = await _uow.Educations.GetAllAsync();
            return _mapper.Map<List<EducationDto>>(data);
        }

        public async Task<EducationDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.Educations.GetByIdAsync(id);
            return _mapper.Map<EducationDto>(entity);
        }

        public async Task<bool> CreateAsync(EducationCreateDto dto)
        {
            var entity = _mapper.Map<Education>(dto);
            await _uow.Educations.AddAsync(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(EducationDto dto)
        {
            var entity = await _uow.Educations.GetByIdAsync(dto.Id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            _uow.Educations.Update(entity);

            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _uow.Educations.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }
    }
}
