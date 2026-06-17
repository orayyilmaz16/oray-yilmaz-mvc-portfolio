using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Repositories;

namespace OrayPortfolio.Web.Controllers
{
    public class VolunteerWorkController : Controller
    {
        private readonly IUnitOfWork _uow;

        public VolunteerWorkController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var volunteerWorks = await _uow.VolunteerWorks.GetAllAsync();
            return View(volunteerWorks);
        }
    }
}
