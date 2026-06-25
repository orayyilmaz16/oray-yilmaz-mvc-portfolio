using Ganss.Xss; // 📌 YENİ
using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.VolunteerWork;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VolunteerWorkController : BaseAdminController // 📌 GÜVENLİK
    {
        private readonly IVolunteerWorkService _service;
        private readonly IFileService _fileService;

        public VolunteerWorkController(IVolunteerWorkService service, IFileService fileService)
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
            return View(new VolunteerWorkCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 📌 GÜVENLİK
        public async Task<IActionResult> Create(VolunteerWorkCreateDto dto, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            var sanitizer = new HtmlSanitizer(); // 📌 GÜVENLİK
            if (!string.IsNullOrEmpty(dto.Description)) dto.Description = sanitizer.Sanitize(dto.Description);

            if (ImageFile != null && ImageFile.Length > 0)
                dto.ImageUrl = await _fileService.UploadAsync(ImageFile, "volunteer");

            await _service.CreateAsync(dto);

            TempData["Success"] = "Gönüllü çalışma başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 📌 GÜVENLİK
        public async Task<IActionResult> Edit(VolunteerWorkUpdateDto dto, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            var sanitizer = new HtmlSanitizer(); // 📌 GÜVENLİK
            if (!string.IsNullOrEmpty(dto.Description)) dto.Description = sanitizer.Sanitize(dto.Description);

            if (ImageFile != null && ImageFile.Length > 0)
                dto.ImageUrl = await _fileService.UploadAsync(ImageFile, "volunteer");
            else
            {
                var existingData = await _service.GetByIdAsync(dto.Id);
                if (existingData != null) dto.ImageUrl = existingData.ImageUrl;
            }

            await _service.UpdateAsync(dto);

            TempData["Success"] = "Gönüllü çalışma başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            TempData["Success"] = "Gönüllü çalışma başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}