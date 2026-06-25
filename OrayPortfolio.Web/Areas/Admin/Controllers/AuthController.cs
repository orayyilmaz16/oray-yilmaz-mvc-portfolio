using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Auth;
using OrayPortfolio.Web.Services;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace OrayPortfolio.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        #region Actions

        [HttpGet]
        public IActionResult Login()
        {
            GenerateQrCode();
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                GenerateQrCode();
                return View(model);
            }

            var adminUser = _config["AdminAuth:Username"];
            var adminHash = _config["AdminAuth:PasswordHash"];
            var twoFactorKey = _config["AdminAuth:TwoFactorKey"];
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            // 1. Rate Limit Kontrolü
            if (RateLimitService.IsBlocked(ip))
            {
                return HandleLoginError("Çok fazla deneme yaptın. 5 dakika sonra tekrar dene.", model);
            }

            // 2. Kimlik Doğrulama
            if (model.Username != adminUser || PasswordHasher.Hash(model.Password) != adminHash)
            {
                RateLimitService.RegisterFail(ip);
                return HandleLoginError("Kullanıcı adı veya şifre hatalı.", model);
            }

            // 3. 2FA Kontrolü
            if (!TwoFactorService.ValidateCode(twoFactorKey, model.Code))
            {
                RateLimitService.RegisterFail(ip);
                return HandleLoginError("2FA kodu hatalı.", model);
            }

            // Başarılı giriş
            RateLimitService.Reset(ip);
            HttpContext.Session.SetString("AdminAuth", "true");

            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminAuth");
            return RedirectToAction("Login", "Auth", new { area = "Admin" });
        }

        #endregion

        #region Helpers

        private void GenerateQrCode()
        {
            var secret = _config["AdminAuth:TwoFactorKey"];
            var otpauth = $"otpauth://totp/OrayPortfolio:Admin?secret={secret}&issuer=OrayPortfolio";

            using var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(otpauth, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrData);

            using Bitmap qrBitmap = qrCode.GetGraphic(20);
            using MemoryStream ms = new MemoryStream();
            qrBitmap.Save(ms, ImageFormat.Png);

            ViewBag.QR = $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
        }

        private IActionResult HandleLoginError(string message, LoginViewModel model)
        {
            GenerateQrCode();
            TempData["Error"] = message;
            return View(model);
        }

        #endregion
    }
}