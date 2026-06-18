namespace OrayPortfolio.Web.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadAsync(IFormFile file, string folder)
        {
            // 🔥 1) Resim hiç seçilmediyse null döndür
            if (file == null || file.Length == 0)
                return null;

            var uploadPath = Path.Combine(_env.WebRootPath, "uploads", folder);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // 🔥 2) FileName'e erişmeden önce file null olmadığından emin olduk
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{folder}/{fileName}";
        }
    }

}
