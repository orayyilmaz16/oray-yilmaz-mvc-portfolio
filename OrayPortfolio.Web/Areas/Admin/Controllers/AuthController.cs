using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public IActionResult Login()
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

        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password, string code)
    {
        var adminUser = _config["AdminAuth:Username"];
        var adminHash = _config["AdminAuth:PasswordHash"];
        var twoFactorKey = _config["AdminAuth:TwoFactorKey"];

        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

        if (RateLimitService.IsBlocked(ip))
        {
            ViewBag.Error = "Çok fazla deneme yaptın. 5 dakika sonra tekrar dene.";
            return View();
        }

        if (username != adminUser || PasswordHasher.Hash(password) != adminHash)
        {
            RateLimitService.RegisterFail(ip);
            ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        if (!TwoFactorService.ValidateCode(twoFactorKey, code))
        {
            RateLimitService.RegisterFail(ip);
            ViewBag.Error = "2FA kodu hatalı.";
            return View();
        }

        RateLimitService.Reset(ip);

        HttpContext.Session.SetString("AdminAuth", "true");

        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
    }



    public IActionResult Logout()
    {
        HttpContext.Session.Remove("AdminAuth");
        return RedirectToAction("Login");
    }
}
