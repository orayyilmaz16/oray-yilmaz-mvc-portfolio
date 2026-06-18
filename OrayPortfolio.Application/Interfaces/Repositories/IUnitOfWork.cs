using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Domain.Entities;

public interface IUnitOfWork
{
    IGenericRepository<Profile> Profiles { get; }
    IGenericRepository<Experience> Experiences { get; }
    IGenericRepository<Skill> Skills { get; }
    IGenericRepository<Education> Educations { get; }
    IGenericRepository<Certificate> Certificates { get; }
    IGenericRepository<Project> Projects { get; }
    IGenericRepository<VolunteerWork> VolunteerWorks { get; }
    IReferenceRepository References { get; }
    Task<int> SaveAsync();
}
