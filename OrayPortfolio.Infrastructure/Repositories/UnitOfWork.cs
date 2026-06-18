using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Domain.Entities;
using OrayPortfolio.Infrastructure.Context;
using OrayPortfolio.Infrastructure.Repositories;

namespace OrayPortfolio.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<Profile> Profiles { get; }
        public IGenericRepository<Experience> Experiences { get; }
        public IGenericRepository<Skill> Skills { get; }
        public IGenericRepository<Education> Educations { get; }
        public IGenericRepository<Certificate> Certificates { get; }
        public IGenericRepository<Project> Projects { get; }
        public IGenericRepository<VolunteerWork> VolunteerWorks { get; }
        public IReferenceRepository References { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;


            Profiles = new GenericRepository<Profile>(context);
            Experiences = new GenericRepository<Experience>(context);
            Skills = new GenericRepository<Skill>(context);
            Educations = new GenericRepository<Education>(context);
            Certificates = new GenericRepository<Certificate>(context);
            Projects = new GenericRepository<Project>(context);
            References = new ReferenceRepository(context);
            VolunteerWorks = new GenericRepository<VolunteerWork>(context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
