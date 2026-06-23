using Microsoft.AspNetCore.Mvc.Filters;
using OrayPortfolio.Application.Interfaces.Services;

namespace OrayPortfolio.Web.Filters
{
    public class VisitorTrackingFilter : IAsyncActionFilter
    {
        private readonly IVisitorService _visitorService;

        public VisitorTrackingFilter(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 📌 KRİTİK: Ziyaretçi "Admin" panelinde geziyorsa, yani sensen BUNU SAYMA!
            var area = context.RouteData.Values["area"]?.ToString();

            if (string.IsNullOrEmpty(area) || !area.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmeyen";

                // Localhost testlerinde ::1 dönebilir, bunu düzeltiyoruz
                if (ip == "::1") ip = "127.0.0.1";

                await _visitorService.LogVisitAsync(ip);
            }

            // İşleme (sayfanın yüklenmesine) devam et
            await next();
        }
    }
}