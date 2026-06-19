using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Services;

namespace OrayPortfolio.Web.Controllers
{
    public class SkillController : Controller
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _skillService.GetAllAsync();
            return View(data);
        }
    }
}
