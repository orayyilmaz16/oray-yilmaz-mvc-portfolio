using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Reference;
using OrayPortfolio.Application.Interfaces.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReferenceController : Controller
    {
        private readonly IReferenceService _service;

        public ReferenceController(IReferenceService service)
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
            return View(new ReferenceCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReferenceCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            await _service.CreateAsync(dto);

            TempData["Success"] = "Referans başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReferenceDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
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
