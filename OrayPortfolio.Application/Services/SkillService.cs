using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OrayPortfolio.Application.DTOs.Skill;
using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SkillService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<SkillUpdateDto>> GetAllAsync()
        {
            var data = await _uow.Skills.GetAllAsync();
            return _mapper.Map<List<SkillUpdateDto>>(data);
        }

        public async Task<SkillUpdateDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.Skills.GetByIdAsync(id);
            if (entity == null)
                return null;

            return _mapper.Map<SkillUpdateDto>(entity);
        }

        public async Task<bool> CreateAsync(SkillCreateDto dto)
        {
            var entity = _mapper.Map<Skill>(dto);
            await _uow.Skills.AddAsync(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(SkillUpdateDto dto)
        {
            var entity = _mapper.Map<Skill>(dto);
            _uow.Skills.Update(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Skills.GetByIdAsync(id);
            if (entity == null)
                return false;

            _uow.Skills.Delete(entity);
            return await _uow.SaveAsync() > 0;
        }
    }
}
