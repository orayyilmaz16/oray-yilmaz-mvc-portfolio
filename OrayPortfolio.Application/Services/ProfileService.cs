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

        public async Task<ProfileDto> GetAsync()
        {
            var entity = (await _uow.Profiles.GetAllAsync()).FirstOrDefault();

            if (entity == null)
            {
                entity = new OrayPortfolio.Domain.Entities.Profile
                {
                    FullName = "",
                    Title = "",
                    ShortBio = "",
                    LongBio = "",
                    Email = "",
                    GithubUrl = "",
                    LinkedinUrl = "",
                    InstagramUrl = "",
                    ProfileImageUrl = "",
                    CvFilePath = "" // 📌 EKLENDİ: İlk oluşturulmada hata vermemesi için
                };

                await _uow.Profiles.AddAsync(entity);
                await _uow.SaveAsync();
            }

            return _mapper.Map<ProfileDto>(entity);
        }

        public async Task<bool> UpdateAsync(ProfileUpdateDto dto)
        {
            var entity = (await _uow.Profiles.GetAllAsync()).FirstOrDefault();
            if (entity == null) return false;

            entity.FullName = dto.FullName;
            entity.Title = dto.Title;
            entity.ShortBio = dto.ShortBio;
            entity.LongBio = dto.LongBio;
            entity.Email = dto.Email;
            entity.GithubUrl = dto.GithubUrl;
            entity.LinkedinUrl = dto.LinkedinUrl;
            entity.InstagramUrl = dto.InstagramUrl;
            entity.ProfileImageUrl = dto.ProfileImageUrl;
            entity.CvFilePath = dto.CvFilePath;

            _uow.Profiles.Update(entity);
            return await _uow.SaveAsync() > 0;
        }
    }
}