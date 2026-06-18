using AutoMapper;
using OrayPortfolio.Application.DTOs.VolunteerWork;
using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Services
{
    public class VolunteerWorkService : IVolunteerWorkService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VolunteerWorkService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<VolunteerWorkUpdateDto>> GetAllAsync()
        {
            var data = await _uow.VolunteerWorks.GetAllAsync();
            return _mapper.Map<List<VolunteerWorkUpdateDto>>(data);
        }

        public async Task<VolunteerWorkUpdateDto> GetByIdAsync(int id)
        {
            var entity = await _uow.VolunteerWorks.GetByIdAsync(id);
            return _mapper.Map<VolunteerWorkUpdateDto>(entity);
        }

        public async Task<bool> CreateAsync(VolunteerWorkCreateDto dto)
        {
            var entity = _mapper.Map<VolunteerWork>(dto);
            await _uow.VolunteerWorks.AddAsync(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(VolunteerWorkUpdateDto dto)
        {
            var entity = await _uow.VolunteerWorks.GetByIdAsync(dto.Id);
            if (entity == null) return false;

            entity.Organization = dto.Organization;
            entity.Role = dto.Role;
            entity.Description = dto.Description;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.IsCurrent = dto.IsCurrent;

            if (!string.IsNullOrWhiteSpace(dto.ImageUrl))
                entity.ImageUrl = dto.ImageUrl;

            _uow.VolunteerWorks.Update(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.VolunteerWorks.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.VolunteerWorks.Delete(entity);
            return await _uow.SaveAsync() > 0;
        }
    }


}
