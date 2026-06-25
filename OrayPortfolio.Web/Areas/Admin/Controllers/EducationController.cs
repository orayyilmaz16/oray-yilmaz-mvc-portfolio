using Ganss.Xss; // 📌 YENİ
using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Education;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : BaseAdminController // 📌 GÜVENLİK
    {
        private readonly IEducationService _service;
        private readonly IFileService _fileService;

        public EducationController(IEducationService service, IFileService fileService)
        {
            _service = service;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View(new EducationCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 📌 GÜVENLİK
        public async Task<IActionResult> Create(EducationCreateDto dto, IFormFile? LogoImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            var sanitizer = new HtmlSanitizer(); // 📌 GÜVENLİK
            if (!string.IsNullOrEmpty(dto.Description)) dto.Description = sanitizer.Sanitize(dto.Description);

            if (LogoImage != null && LogoImage.Length > 0)
                dto.LogoImageUrl = await _fileService.UploadAsync(LogoImage, "educations");

            await _service.CreateAsync(dto);

            TempData["Success"] = "Eğitim başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 📌 GÜVENLİK
        public async Task<IActionResult> Edit(EducationDto dto, IFormFile? LogoImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            var sanitizer = new HtmlSanitizer(); // 📌 GÜVENLİK
            if (!string.IsNullOrEmpty(dto.Description)) dto.Description = sanitizer.Sanitize(dto.Description);

            if (LogoImage != null && LogoImage.Length > 0)
            {
                dto.LogoImageUrl = await _fileService.UploadAsync(LogoImage, "educations");
            }
            else
            {
                var existingEdu = await _service.GetByIdAsync(dto.Id);
                if (existingEdu != null) dto.LogoImageUrl = existingEdu.LogoImageUrl;
            }

            await _service.UpdateAsync(dto);

            TempData["Success"] = "Eğitim başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            TempData["Success"] = "Eğitim başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}