using AutoMapper;
using OrayPortfolio.Application.DTOs.Certificate;
using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CertificateService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<CertificateUpdateDto>> GetAllAsync()
        {
            var data = await _uow.Certificates.GetAllAsync();
            return _mapper.Map<List<CertificateUpdateDto>>(data);
        }

        public async Task<CertificateUpdateDto> GetByIdAsync(int id)
        {
            var entity = await _uow.Certificates.GetByIdAsync(id);
            return _mapper.Map<CertificateUpdateDto>(entity);
        }

        public async Task<bool> CreateAsync(CertificateCreateDto dto)
        {
            var entity = _mapper.Map<Certificate>(dto);
            await _uow.Certificates.AddAsync(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(CertificateUpdateDto dto)
        {
            var entity = await _uow.Certificates.GetByIdAsync(dto.Id);
            if (entity == null) return false;

            entity.Title = dto.Title;
            entity.Issuer = dto.Issuer;
            entity.Date = dto.Date;
            entity.CertificateUrl = dto.CertificateUrl;

            // 🔥 Yeni dosya geldiyse değiştir
            if (!string.IsNullOrWhiteSpace(dto.FileUrl))
                entity.FileUrl = dto.FileUrl;

            _uow.Certificates.Update(entity);
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Certificates.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Certificates.Delete(entity);
            return await _uow.SaveAsync() > 0;
        }
    }
}
