using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Domain.Entities;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MediaController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMediaService _mediaService;

        public MediaController(IWebHostEnvironment env, IMediaService mediaService)
        {
            _env = env;
            _mediaService = mediaService;
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string folder, int? projectId)
        {
            if (file == null)
                return View();

            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", folder);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string extension = Path.GetExtension(file.FileName);
            string storedFileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(uploadsFolder, storedFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var media = new Media
            {
                FileName = file.FileName,
                StoredFileName = storedFileName,
                FilePath = $"/uploads/{folder}/{storedFileName}",
                FileType = file.ContentType,
                ProjectId = projectId
            };

            await _mediaService.CreateMediaRecordAsync(media);

            return RedirectToAction("Upload");
        }
    }
}
