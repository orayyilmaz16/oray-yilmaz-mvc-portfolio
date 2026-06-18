using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Domain.Entities;
using OrayPortfolio.Infrastructure.Context;

namespace OrayPortfolio.Infrastructure.Repositories
{
    public class ReferenceRepository : GenericRepository<Reference>, IReferenceRepository
    {
        public ReferenceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
