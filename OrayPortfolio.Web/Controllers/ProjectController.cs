using Ganss.Xss; // 📌 YENİ: XSS Koruması için eklendi
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

        // 📌 PERFORMANS & DOS KORUMASI: Bu sayfa 60 saniye boyunca önbellekte (Cache) tutulur. 
        // Böylece 1 dakikada 1000 kişi bile girse veritabanına sadece 1 kez sorgu gider!
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> Index()
        {
            var projects = await _uow.Projects.GetAllAsync();
            var sanitizer = new HtmlSanitizer();

            // 📌 XSS KORUMASI: Tüm projelerin açıklama kısımlarındaki zararlı scriptleri ayıklar
            foreach (var project in projects)
            {
                if (!string.IsNullOrEmpty(project.Description))
                {
                    project.Description = sanitizer.Sanitize(project.Description);
                }
            }

            return View(projects);
        }

        // 📌 PERFORMANS: Detay sayfası da ID bazlı olarak 60 saniye önbelleğe alınır.
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Detail(int id)
        {
            var project = await _uow.Projects.GetByIdAsync(id);

            if (project == null)
                return NotFound();

            // 📌 XSS KORUMASI: Sadece tıklanan projenin içeriğini temizle ve güvenli HTML'i View'a gönder
            if (!string.IsNullOrEmpty(project.Description))
            {
                var sanitizer = new HtmlSanitizer();
                project.Description = sanitizer.Sanitize(project.Description);
            }

            return View(project);
        }
    }
}