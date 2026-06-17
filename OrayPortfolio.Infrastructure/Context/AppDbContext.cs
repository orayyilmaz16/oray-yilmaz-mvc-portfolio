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
        public DbSet<Media> MediaFiles { get; set; }
        public IEnumerable<object> Media { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Media ilişkileri
            modelBuilder.Entity<Media>()
                .HasOne(m => m.Project)
                .WithMany(p => p.MediaFiles)
                .HasForeignKey(m => m.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Experience)
                .WithMany(e => e.MediaFiles)
                .HasForeignKey(m => m.ExperienceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Certificate)
                .WithMany(c => c.MediaFiles)
                .HasForeignKey(m => m.CertificateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.VolunteerWork)
                .WithMany(v => v.MediaFiles)
                .HasForeignKey(m => m.VolunteerWorkId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
