using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Application.DTOs.Skill;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly ISkillService _service;

        public SkillController(ISkillService service)
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
            return View(new SkillCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SkillCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            await _service.CreateAsync(dto);

            TempData["Success"] = "Yetenek başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SkillUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(dto);
            }

            await _service.UpdateAsync(dto);

            TempData["Success"] = "Yetenek başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            TempData["Success"] = "Yetenek başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}

