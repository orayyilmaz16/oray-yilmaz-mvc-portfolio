using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Education;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly IEducationService _service;
        private readonly IFileService _fileService; // 📌 Dosya yükleme servisi eklendi

        public EducationController(IEducationService service, IFileService fileService)
        {
            _service = service;
            _fileService = fileService; // 📌 Servis atandı
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

        // 📌 POST: Yeni eğitim eklenirken IFormFile eklendi
        [HttpPost]
        public async Task<IActionResult> Create(EducationCreateDto dto, IFormFile? LogoImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            // 📌 Eğer resim seçildiyse sunucuya yükle ve klasör adını "educations" yap
            if (LogoImage != null && LogoImage.Length > 0)
            {
                dto.LogoImageUrl = await _fileService.UploadAsync(LogoImage, "educations");
            }

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

        // 📌 POST: Düzenleme yapılırken IFormFile eklendi
        [HttpPost]
        public async Task<IActionResult> Edit(EducationDto dto, IFormFile? LogoImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            // 📌 Eğer YENİ bir resim seçildiyse yükle
            if (LogoImage != null && LogoImage.Length > 0)
            {
                dto.LogoImageUrl = await _fileService.UploadAsync(LogoImage, "educations");
            }
            else
            {
                // 📌 Eğer yeni resim SEÇİLMEDİYSE mevcut resmi kaybetmemek için veritabanından bul ve dto'ya ata
                var existingEdu = await _service.GetByIdAsync(dto.Id);
                if (existingEdu != null)
                {
                    dto.LogoImageUrl = existingEdu.LogoImageUrl;
                }
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