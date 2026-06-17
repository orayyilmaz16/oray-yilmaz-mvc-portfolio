using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Profile;
using OrayPortfolio.Application.Interfaces.Services;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
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
            if (ProfileImage != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ProfileImage.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/profile", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                model.ProfileImageUrl = "/uploads/profile/" + fileName;
            }

            await _profileService.UpdateAsync(model);

            TempData["success"] = "Profil güncellendi";
            return RedirectToAction("Index");
        }
    }
}
