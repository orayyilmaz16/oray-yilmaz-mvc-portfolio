using Microsoft.AspNetCore.Mvc;

namespace OrayPortfolio.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
