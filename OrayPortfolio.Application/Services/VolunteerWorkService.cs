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

        public async Task<List<VolunteerWorkDto>> GetAllAsync()
        {
            var data = await _uow.VolunteerWorks.GetAllAsync();
            return _mapper.Map<List<VolunteerWorkDto>>(data);
        }

        public async Task<VolunteerWorkDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.VolunteerWorks.GetByIdAsync(id);
            return _mapper.Map<VolunteerWorkDto>(entity);
        }

        public async Task<bool> CreateAsync(VolunteerWorkCreateDto dto)
        {
            var entity = _mapper.Map<VolunteerWork>(dto);
            await _uow.VolunteerWorks.AddAsync(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(VolunteerWorkDto dto)
        {
            var entity = await _uow.VolunteerWorks.GetByIdAsync(dto.Id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            _uow.VolunteerWorks.Update(entity);

            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _uow.VolunteerWorks.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }
    }

}
