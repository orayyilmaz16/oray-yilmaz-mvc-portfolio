using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Education;
using OrayPortfolio.Application.Interfaces.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly IEducationService _service;

        public EducationController(IEducationService service)
        {
            _service = service;
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
        public async Task<IActionResult> Create(EducationCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            await _service.CreateAsync(dto);

            TempData["Success"] = "Eğitim başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EducationDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
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
