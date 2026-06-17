using Microsoft.AspNetCore.Mvc;

namespace OrayPortfolio.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ProfileController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var profiles = await _uow.Profiles.GetAllAsync();
            var profile = profiles.FirstOrDefault();

            if (profile == null)
                return NotFound();

            try
            {
                return View(profile);
            }
            catch (Exception ex)
            {
                return Content("VIEW HATASI: " + ex.Message);
            }
        }

    }
}
