using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Profile;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Web.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IFileService _fileService;

        public ProfileController(IProfileService profileService, IFileService fileService)
        {
            _profileService = profileService;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _profileService.GetAsync();

            var model = new ProfileUpdateDto
            {
                Id = data.Id,
                FullName = data.FullName,
                Title = data.Title,
                ShortBio = data.ShortBio,
                LongBio = data.LongBio,
                Email = data.Email,
                GithubUrl = data.GithubUrl,
                LinkedinUrl = data.LinkedinUrl,
                InstagramUrl = data.InstagramUrl,
                ProfileImageUrl = data.ProfileImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProfileUpdateDto model, IFormFile? ProfileImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(model);
            }

            // Yeni fotoğraf varsa yükle
            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                model.ProfileImageUrl = await _fileService.UploadAsync(ProfileImage, "profile");
            }

            await _profileService.UpdateAsync(model);

            TempData["Success"] = "Profil başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
