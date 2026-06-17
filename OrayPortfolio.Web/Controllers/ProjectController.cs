using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Repositories;

namespace OrayPortfolio.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ProjectController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _uow.Projects.GetAllAsync();
            return View(projects);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var project = await _uow.Projects.GetByIdAsync(id);
            if (project == null)
                return NotFound();

            return View(project);
        }
    }
}
