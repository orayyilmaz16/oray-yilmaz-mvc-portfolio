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

        // PROFİLİ GETİR — YOKSA OLUŞTUR
        public async Task<ProfileDto> GetAsync()
        {
            var entity = (await _uow.Profiles.GetAllAsync()).FirstOrDefault();

            // Profil yoksa otomatik oluştur
            if (entity == null)
            {
                entity = new Domain.Entities.Profile
                {
                    FullName = "",
                    Title = "",
                    ShortBio = "",
                    LongBio = "",
                    Email = "",
                    GithubUrl = "",
                    LinkedinUrl = "",
                    InstagramUrl = "",
                    ProfileImageUrl = ""
                };

                await _uow.Profiles.AddAsync(entity);
                await _uow.SaveAsync();
            }

            return _mapper.Map<ProfileDto>(entity);
        }

        // PROFİLİ GÜNCELLE
        public async Task<bool> UpdateAsync(ProfileUpdateDto dto)
        {
            var entity = await _uow.Profiles.GetByIdAsync(dto.Id);

            if (entity == null)
                return false;

            _mapper.Map(dto, entity);

            _uow.Profiles.Update(entity);
            return await _uow.SaveAsync() > 0;
        }
    }
}
