using Ganss.Xss; // 📌 YENİ
using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Project;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : BaseAdminController // 📌 GÜVENLİK
    {
        private readonly IProjectService _projectService;
        private readonly IFileService _fileService;

        public ProjectController(IProjectService projectService, IFileService fileService)
        {
            _projectService = projectService;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _projectService.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View(new ProjectCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 📌 GÜVENLİK
        public async Task<IActionResult> Create(ProjectCreateDto dto, IFormFile? CoverImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            var sanitizer = new HtmlSanitizer(); // 📌 GÜVENLİK
            if (!string.IsNullOrEmpty(dto.Description)) dto.Description = sanitizer.Sanitize(dto.Description);

            if (CoverImage != null && CoverImage.Length > 0)
                dto.CoverImageUrl = await _fileService.UploadAsync(CoverImage, "projects");

            await _projectService.CreateAsync(dto);

            TempData["Success"] = "Proje başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _projectService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 📌 GÜVENLİK
        public async Task<IActionResult> Edit(ProjectUpdateDto dto, IFormFile? CoverImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            var sanitizer = new HtmlSanitizer(); // 📌 GÜVENLİK
            if (!string.IsNullOrEmpty(dto.Description)) dto.Description = sanitizer.Sanitize(dto.Description);

            if (CoverImage != null && CoverImage.Length > 0)
                dto.CoverImageUrl = await _fileService.UploadAsync(CoverImage, "projects");
            else
            {
                var existingData = await _projectService.GetByIdAsync(dto.Id);
                if (existingData != null) dto.CoverImageUrl = existingData.CoverImageUrl;
            }

            await _projectService.UpdateAsync(dto);

            TempData["Success"] = "Proje başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _projectService.DeleteAsync(id);
            TempData["Success"] = "Proje başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}