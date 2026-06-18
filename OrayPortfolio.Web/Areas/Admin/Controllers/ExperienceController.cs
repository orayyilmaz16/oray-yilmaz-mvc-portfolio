using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Experience;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
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
        public async Task<IActionResult> Create(ExperienceCreateDto dto, IFormFile? LogoImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

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
        public async Task<IActionResult> Edit(ExperienceUpdateDto dto, IFormFile? LogoImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            if (LogoImage != null && LogoImage.Length > 0)
                dto.LogoImageUrl = await _fileService.UploadAsync(LogoImage, "experiences");

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
