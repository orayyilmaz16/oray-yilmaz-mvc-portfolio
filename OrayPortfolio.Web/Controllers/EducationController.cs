using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Services;

namespace OrayPortfolio.Web.Controllers
{
    public class EducationController : Controller
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _educationService.GetAllAsync();
            return View(data);
        }
    }
}
