using AutoMapper;
using OrayPortfolio.Application.DTOs.Skill;
using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Domain.Entities;

public class SkillService : ISkillService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public SkillService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<SkillDto>> GetAllAsync()
    {
        var data = await _uow.Skills.GetAllAsync();
        return _mapper.Map<List<SkillDto>>(data);
    }

    public async Task<bool> CreateAsync(SkillDto dto)
    {
        var entity = _mapper.Map<Skill>(dto);
        await _uow.Skills.AddAsync(entity);
        return await _uow.SaveAsync() > 0;
    }

    public async Task<bool> UpdateAsync(SkillDto dto)
    {
        var entity = _mapper.Map<Skill>(dto);
        _uow.Skills.Update(entity);
        return await _uow.SaveAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _uow.Skills.GetByIdAsync(id);
        if (entity == null) return false;

        _uow.Skills.Delete(entity);
        return await _uow.SaveAsync() > 0;
    }

    public async Task<SkillDto?> GetByIdAsync(int id)
    {
        var entity = await _uow.Skills.GetByIdAsync(id);
        return _mapper.Map<SkillDto?>(entity);
    }

}
