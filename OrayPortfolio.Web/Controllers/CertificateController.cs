using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Repositories;

namespace OrayPortfolio.Web.Controllers
{
    public class CertificateController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CertificateController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var certificates = await _uow.Certificates.GetAllAsync();
            return View(certificates);
        }
    }
}
