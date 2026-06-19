using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Services;

namespace OrayPortfolio.Web.Controllers
{
    public class ReferenceController : Controller
    {
        private readonly IReferenceService _referenceService;

        public ReferenceController(IReferenceService referenceService)
        {
            _referenceService = referenceService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _referenceService.GetAllAsync();
            return View(data);
        }
    }
}
