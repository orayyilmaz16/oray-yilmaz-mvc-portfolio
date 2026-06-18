using AutoMapper;
using OrayPortfolio.Application.DTOs.Reference;
using OrayPortfolio.Domain.Entities;

public class ReferenceProfile : AutoMapper.Profile
{
    public ReferenceProfile()
    {
        CreateMap<Reference, ReferenceDto>().ReverseMap();
        CreateMap<Reference, ReferenceCreateDto>().ReverseMap();
    }
}
