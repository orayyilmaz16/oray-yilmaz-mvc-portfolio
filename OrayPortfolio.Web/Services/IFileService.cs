using Microsoft.AspNetCore.Http;

namespace OrayPortfolio.Web.Services
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file, string folder);
    }
}
