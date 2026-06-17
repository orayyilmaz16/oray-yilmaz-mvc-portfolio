using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Repositories;

namespace OrayPortfolio.Web.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ExperienceController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var experiences = await _uow.Experiences.GetAllAsync();
            return View(experiences);
        }
    }
}
