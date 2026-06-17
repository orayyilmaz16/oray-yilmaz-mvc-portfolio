using AutoMapper;
using OrayPortfolio.Application.DTOs.Profile;
using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProfileService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ProfileUpdateDto> GetAsync()
        {
            var entity = (await _uow.Profiles.GetAllAsync()).FirstOrDefault();
            return _mapper.Map<ProfileUpdateDto>(entity);
        }

        public async Task<bool> UpdateAsync(ProfileUpdateDto dto)
        {
            var entity = _mapper.Map<Domain.Entities.Profile>(dto);
            _uow.Profiles.Update(entity);
            return await _uow.SaveAsync() > 0;
        }
    }
}
