using Ganss.Xss; // 📌 YENİ
using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Certificate;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CertificateController : BaseAdminController // 📌 GÜVENLİK: BaseAdminController olarak değiştirildi
    {
        private readonly ICertificateService _certificateService;
        private readonly IFileService _fileService;

        public CertificateController(ICertificateService certificateService, IFileService fileService)
        {
            _certificateService = certificateService;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _certificateService.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View(new CertificateCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 📌 GÜVENLİK
        public async Task<IActionResult> Create(CertificateCreateDto dto, IFormFile? FileUpload)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            var sanitizer = new HtmlSanitizer(); // 📌 GÜVENLİK
            if (!string.IsNullOrEmpty(dto.Title)) dto.Title = sanitizer.Sanitize(dto.Title);
            

            if (FileUpload != null && FileUpload.Length > 0)
                dto.FileUrl = await _fileService.UploadAsync(FileUpload, "certificates");

            await _certificateService.CreateAsync(dto);

            TempData["Success"] = "Sertifika başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _certificateService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 📌 GÜVENLİK
        public async Task<IActionResult> Edit(CertificateUpdateDto dto, IFormFile? FileUpload)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            var sanitizer = new HtmlSanitizer(); // 📌 GÜVENLİK
            if (!string.IsNullOrEmpty(dto.Title)) dto.Title = sanitizer.Sanitize(dto.Title);
         

            if (FileUpload != null && FileUpload.Length > 0)
                dto.FileUrl = await _fileService.UploadAsync(FileUpload, "certificates");
            else
            {
                var existingData = await _certificateService.GetByIdAsync(dto.Id);
                if (existingData != null) dto.FileUrl = existingData.FileUrl;
            }

            await _certificateService.UpdateAsync(dto);

            TempData["Success"] = "Sertifika başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _certificateService.DeleteAsync(id);

            TempData["Success"] = "Sertifika başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}