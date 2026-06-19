using Microsoft.AspNetCore.Mvc;
using OrayPortfolio.Application.DTOs.Auth;
using OrayPortfolio.Web.Services;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

[Area("Admin")]
public class AuthController : Controller
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    // 🔥 QR CODE ÜRETİMİ (Tek Nokta)
    private void GenerateQrCode()
    {
        var secret = _config["AdminAuth:TwoFactorKey"];
        var issuer = "OrayPortfolio";
        var label = "Admin";

        string otpauth = $"otpauth://totp/{issuer}:{label}?secret={secret}&issuer={issuer}";

        using var qrGenerator = new QRCodeGenerator();
        var qrData = qrGenerator.CreateQrCode(otpauth, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new QRCode(qrData);

        using Bitmap qrBitmap = qrCode.GetGraphic(20);
        using MemoryStream ms = new MemoryStream();
        qrBitmap.Save(ms, ImageFormat.Png);

        ViewBag.QR = $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
    }

    // 🔥 LOGIN GET
    [HttpGet]
    public IActionResult Login()
    {
        GenerateQrCode();
        return View(new LoginViewModel());
    }

    // 🔥 LOGIN POST
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        GenerateQrCode();

        // FluentValidation hataları → Toastr ile gösterilecek
        if (!ModelState.IsValid)
            return View(model);

        var adminUser = _config["AdminAuth:Username"];
        var adminHash = _config["AdminAuth:PasswordHash"];
        var twoFactorKey = _config["AdminAuth:TwoFactorKey"];

        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

        // Rate Limit Kontrolü
        if (RateLimitService.IsBlocked(ip))
        {
            TempData["Error"] = "Çok fazla deneme yaptın. 5 dakika sonra tekrar dene.";
            return View(model);
        }

        // Username + Password Kontrolü
        if (model.Username != adminUser || PasswordHasher.Hash(model.Password) != adminHash)
        {
            RateLimitService.RegisterFail(ip);
            TempData["Error"] = "Kullanıcı adı veya şifre hatalı.";
            return View(model);
        }

        // 2FA Kontrolü
        if (!TwoFactorService.ValidateCode(twoFactorKey, model.Code))
        {
            RateLimitService.RegisterFail(ip);
            TempData["Error"] = "2FA kodu hatalı.";
            return View(model);
        }

        // Başarılı giriş
        RateLimitService.Reset(ip);
        HttpContext.Session.SetString("AdminAuth", "true");

        TempData["Success"] = "Başarıyla giriş yaptın!";
        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
    }

    // Log Out
    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("AdminAuth");
        TempData["Success"] = "Başarıyla çıkış yaptın!";
        return RedirectToAction("Login", "Auth", new { area = "Admin" });
    }
}
