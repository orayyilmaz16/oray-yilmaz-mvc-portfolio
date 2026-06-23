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
                ProfileImageUrl = data.ProfileImageUrl,
                CvFilePath = data.CvFilePath // 📌 EKLENDİ
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProfileUpdateDto model, IFormFile? ProfileImage, IFormFile? CvFile)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen formdaki hataları düzeltin.";
                return View(model);
            }

            // 📌 KRİTİK GÜVENLİK: Mevcut veritabanı kaydını çekiyoruz
            // Kullanıcı yeni dosya seçmezse, eski dosyaların silinmesini (null olmasını) engelleyeceğiz.
            var existingProfile = await _profileService.GetAsync();

            // 📌 Yeni profil fotoğrafı varsa yükle, YOKSA ESKİYİ KORU
            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                model.ProfileImageUrl = await _fileService.UploadAsync(ProfileImage, "profile");
            }
            else
            {
                model.ProfileImageUrl = existingProfile.ProfileImageUrl;
            }

            // 📌 Yeni CV varsa yükle, YOKSA ESKİYİ KORU
            if (CvFile != null && CvFile.Length > 0)
            {
                model.CvFilePath = await _fileService.UploadAsync(CvFile, "cv");
            }
            else
            {
                model.CvFilePath = existingProfile.CvFilePath;
            }

            await _profileService.UpdateAsync(model);

            TempData["Success"] = "Profil başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFile(string fileType)
        {
            var existingProfile = await _profileService.GetAsync();

            // Mevcut verileri DTO'ya dolduruyoruz (Silinmeyenler kaybolmasın diye)
            var model = new ProfileUpdateDto
            {
                Id = existingProfile.Id,
                FullName = existingProfile.FullName,
                Title = existingProfile.Title,
                ShortBio = existingProfile.ShortBio,
                LongBio = existingProfile.LongBio,
                Email = existingProfile.Email,
                GithubUrl = existingProfile.GithubUrl,
                LinkedinUrl = existingProfile.LinkedinUrl,
                InstagramUrl = existingProfile.InstagramUrl,
                ProfileImageUrl = existingProfile.ProfileImageUrl,
                CvFilePath = existingProfile.CvFilePath
            };

            // Gelen komuta göre sadece ilgili alanı NULL yapıyoruz
            if (fileType == "profile")
            {
                // TODO: İleride _fileService.DeleteAsync() yazarak sunucudaki resmi fiziksel olarak da sildirebilirsin
                model.ProfileImageUrl = null;
                TempData["Success"] = "Profil fotoğrafı başarıyla kaldırıldı.";
            }
            else if (fileType == "cv")
            {
                model.CvFilePath = null;
                TempData["Success"] = "CV dosyası başarıyla kaldırıldı.";
            }

            // Güncel (Null içeren) modeli veritabanına kaydet
            await _profileService.UpdateAsync(model);

            return RedirectToAction("Index");
        }
    }
}