using OrayPortfolio.Application.DTOs.Dashboard;

namespace OrayPortfolio.Application.Interfaces.Services
{
    public interface IVisitorService
    {
        Task LogVisitAsync(string ipAddress);
        Task<DashboardVisitorStatsDto> GetVisitorStatsAsync();
    }
}