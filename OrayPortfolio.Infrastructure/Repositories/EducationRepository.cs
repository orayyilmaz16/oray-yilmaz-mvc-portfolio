using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Domain.Entities;
using OrayPortfolio.Infrastructure.Context;

namespace OrayPortfolio.Infrastructure.Repositories
{
    public class EducationRepository : GenericRepository<Education>, IEducationRepository
    {
        public EducationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
