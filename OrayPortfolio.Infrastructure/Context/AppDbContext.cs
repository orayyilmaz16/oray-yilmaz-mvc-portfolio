using Microsoft.EntityFrameworkCore;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet'ler
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Project> Projects { get; set; }    
        public DbSet<VolunteerWork> VolunteerWorks { get; set; }
        public IEnumerable<object> Media { get; internal set; }

        public DbSet<Reference> References { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Media ilişkileri
           

         

            base.OnModelCreating(modelBuilder);
        }
    }
}
