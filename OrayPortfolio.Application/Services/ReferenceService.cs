using AutoMapper;
using OrayPortfolio.Application.DTOs.Reference;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Domain.Entities;


namespace OrayPortfolio.Application.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ReferenceService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ReferenceDto>> GetAllAsync()
        {
            var data = await _uow.References.GetAllAsync();
            return _mapper.Map<List<ReferenceDto>>(data);
        }

        public async Task<ReferenceDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.References.GetByIdAsync(id);
            return _mapper.Map<ReferenceDto>(entity);
        }

        public async Task<bool> CreateAsync(ReferenceCreateDto dto)
        {
            var entity = _mapper.Map<Reference>(dto);
            await _uow.References.AddAsync(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ReferenceDto dto)
        {
            var entity = await _uow.References.GetByIdAsync(dto.Id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            _uow.References.Update(entity);

            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _uow.References.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }
    }
}
