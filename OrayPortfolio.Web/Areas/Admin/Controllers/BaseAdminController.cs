using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize] // ❌ BURAYI SİL! Bu, kontrolcünün içine girmeden seni dışarı atar.
    public class BaseAdminController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();

            // 📌 GÜVENLİK: Eğer Auth/Login sayfasındaysan veya Auth/Logout yapıyorsan döngüye girme
            if (controller == "Auth" && (action == "Login" || action == "Logout"))
            {
                base.OnActionExecuting(context);
                return;
            }

            // Session kontrolü (Yetkisiz erişimi engelle)
            if (context.HttpContext.Session.GetString("AdminAuth") != "true")
            {
                context.Result = new RedirectToActionResult("Login", "Auth", new { area = "Admin" });
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}