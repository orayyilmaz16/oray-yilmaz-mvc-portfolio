using AutoMapper;
using OrayPortfolio.Application.DTOs.Experience;
using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ExperienceService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ExperienceUpdateDto>> GetAllAsync()
        {
            var data = await _uow.Experiences.GetAllAsync();
            return _mapper.Map<List<ExperienceUpdateDto>>(data);
        }

        public async Task<ExperienceUpdateDto> GetByIdAsync(int id)
        {
            var entity = await _uow.Experiences.GetByIdAsync(id);
            return _mapper.Map<ExperienceUpdateDto>(entity);
        }

        public async Task<bool> CreateAsync(ExperienceCreateDto dto)
        {
            var entity = _mapper.Map<Experience>(dto);
            await _uow.Experiences.AddAsync(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ExperienceUpdateDto dto)
        {
            var entity = await _uow.Experiences.GetByIdAsync(dto.Id);
            if (entity == null) return false;

            // 🔥 Sadece değişen alanları güncelle
            entity.Company = dto.Company;
            entity.Position = dto.Position;
            entity.Description = dto.Description;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.IsCurrent = dto.IsCurrent;

            // 🔥 Logo sadece yeni yüklenmişse değişsin
            if (!string.IsNullOrWhiteSpace(dto.LogoImageUrl))
                entity.LogoImageUrl = dto.LogoImageUrl;

            _uow.Experiences.Update(entity);
            return await _uow.SaveAsync() > 0;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Experiences.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Experiences.Delete(entity);
            return await _uow.SaveAsync() > 0;
        }
    }
}
