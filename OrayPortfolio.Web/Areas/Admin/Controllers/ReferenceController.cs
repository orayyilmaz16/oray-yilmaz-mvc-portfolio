using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Reference;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReferenceController : Controller
    {
        private readonly IReferenceService _service;
        private readonly IFileService _fileService; // 📌 Dosya yükleme servisi eklendi

        public ReferenceController(IReferenceService service, IFileService fileService)
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
            return View(new ReferenceCreateDto());
        }

        // 📌 CREATE POST: Yeni referans eklenirken fotoğraf varsa yükle
        [HttpPost]
        public async Task<IActionResult> Create(ReferenceCreateDto dto, IFormFile? ProfileImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            // Dosya seçilmişse sunucuya yükle ve yolunu DTO'ya ata
            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                dto.ProfileImageUrl = await _fileService.UploadAsync(ProfileImage, "references");
            }

            await _service.CreateAsync(dto);

            TempData["Success"] = "Referans başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();

            return View(data);
        }

        // 📌 EDIT POST: Güncelleme yapılırken eski fotoğrafı koru veya yenisini yükle
        [HttpPost]
        public async Task<IActionResult> Edit(ReferenceDto dto, IFormFile? ProfileImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            // Eğer yeni bir fotoğraf yüklendiyse:
            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                dto.ProfileImageUrl = await _fileService.UploadAsync(ProfileImage, "references");
            }
            else
            {
                // 📌 KRİTİK NOKTA: Yeni fotoğraf seçilmediyse mevcut olanı veritabanından çekip koruyoruz (Null olmasını engelliyoruz)
                var existingReference = await _service.GetByIdAsync(dto.Id);
                if (existingReference != null)
                {
                    dto.ProfileImageUrl = existingReference.ProfileImageUrl;
                }
            }

            await _service.UpdateAsync(dto);

            TempData["Success"] = "Referans başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            TempData["Success"] = "Referans başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}