using AutoMapper;
using OrayPortfolio.Application.DTOs.Certificate;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Application.Mapping
{
    public class CertificateProfile : AutoMapper.Profile
    {
        public CertificateProfile()
        {
            CreateMap<Certificate, CertificateUpdateDto>().ReverseMap();

            CreateMap<CertificateCreateDto, Certificate>()
                .ForMember(dest => dest.FileUrl, opt => opt.MapFrom(src => src.FileUrl));

            CreateMap<CertificateUpdateDto, Certificate>()
                .ForMember(dest => dest.FileUrl, opt => opt.MapFrom(src => src.FileUrl));
        }
    }
}
