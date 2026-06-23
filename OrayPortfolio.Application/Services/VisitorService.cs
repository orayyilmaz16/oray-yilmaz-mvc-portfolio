using OrayPortfolio.Application.DTOs.Dashboard;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Domain.Entities;
// IUnitOfWork namespace'in eksikse buraya using ile ekle (Örn: using OrayPortfolio.Application.Interfaces;)

namespace OrayPortfolio.Application.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IUnitOfWork _uow; // 📌 AppDbContext YERİNE UNIT OF WORK KULLANIYORUZ

        public VisitorService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task LogVisitAsync(string ipAddress)
        {
            var today = DateTime.Today;

            // Tüm logları UOW üzerinden çekip bugün bu IP ile girilmiş mi kontrol et
            var allLogs = await _uow.VisitorLogs.GetAllAsync();
            var exists = allLogs.Any(v => v.IpAddress == ipAddress && v.VisitDate.Date == today);

            if (!exists)
            {
                await _uow.VisitorLogs.AddAsync(new VisitorLog { IpAddress = ipAddress, VisitDate = DateTime.Now });
                await _uow.SaveAsync();
            }
        }

        public async Task<DashboardVisitorStatsDto> GetVisitorStatsAsync()
        {
            var today = DateTime.Today;
            var sevenDaysAgo = today.AddDays(-6);

            // 📌 Verileri DbContext yerine UnitOfWork üzerinden çekiyoruz
            var allLogs = await _uow.VisitorLogs.GetAllAsync();

            var stats = new DashboardVisitorStatsDto
            {
                TotalVisitors = allLogs.Count(),
                TodayVisitors = allLogs.Count(v => v.VisitDate.Date == today)
            };

            // Son 7 günün listesini filtrele
            var last7DaysLogs = allLogs.Where(v => v.VisitDate.Date >= sevenDaysAgo).ToList();

            // 7 günlük döngü ile grafik verilerini hazırla
            for (int i = 6; i >= 0; i--)
            {
                var date = today.AddDays(-i);
                stats.WeeklyVisitorDates.Add(date.ToString("ddd", new System.Globalization.CultureInfo("tr-TR")));
                stats.WeeklyVisitorCounts.Add(last7DaysLogs.Count(l => l.VisitDate.Date == date));
            }

            return stats;
        }
    }
}