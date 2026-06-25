using Ganss.Xss; // 📌 YENİ
using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Experience;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : BaseAdminController // 📌 GÜVENLİK
    {
        private readonly IExperienceService _experienceService;
        private readonly IFileService _fileService;

        public ExperienceController(IExperienceService experienceService, IFileService fileService)
        {
            _experienceService = experienceService;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _experienceService.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View(new ExperienceCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 📌 GÜVENLİK
        public async Task<IActionResult> Create(ExperienceCreateDto dto, IFormFile? LogoImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            var sanitizer = new HtmlSanitizer(); // 📌 GÜVENLİK
            if (!string.IsNullOrEmpty(dto.Description)) dto.Description = sanitizer.Sanitize(dto.Description);

            if (LogoImage != null && LogoImage.Length > 0)
                dto.LogoImageUrl = await _fileService.UploadAsync(LogoImage, "experiences");

            await _experienceService.CreateAsync(dto);

            TempData["Success"] = "Deneyim başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _experienceService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 📌 GÜVENLİK
        public async Task<IActionResult> Edit(ExperienceUpdateDto dto, IFormFile? LogoImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            var sanitizer = new HtmlSanitizer(); // 📌 GÜVENLİK
            if (!string.IsNullOrEmpty(dto.Description)) dto.Description = sanitizer.Sanitize(dto.Description);

            if (LogoImage != null && LogoImage.Length > 0)
                dto.LogoImageUrl = await _fileService.UploadAsync(LogoImage, "experiences");
            else
            {
                var existingData = await _experienceService.GetByIdAsync(dto.Id);
                if (existingData != null) dto.LogoImageUrl = existingData.LogoImageUrl;
            }

            await _experienceService.UpdateAsync(dto);

            TempData["Success"] = "Deneyim başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _experienceService.DeleteAsync(id);
            TempData["Success"] = "Deneyim başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}