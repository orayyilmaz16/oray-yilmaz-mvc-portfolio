using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies; // 📌 YENİ: Kimlik Doğrulama İçin
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Application.Mapping;
using OrayPortfolio.Application.Services;
using OrayPortfolio.Application.Validations;
using OrayPortfolio.Infrastructure.Context;
using OrayPortfolio.Infrastructure.Repositories;
using OrayPortfolio.Infrastructure.UnitOfWork;
using OrayPortfolio.Web.Filters;
using OrayPortfolio.Web.Services;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// 📌 GÜVENLİK: Rate Limiter (Spam Engelleme)
// ----------------------
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "fixed", limiterOptions =>
    {
        limiterOptions.PermitLimit = 20;
        limiterOptions.Window = TimeSpan.FromMinutes(1);
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 2;
    });
});

// ----------------------
// 📌 GÜVENLİK: Cookie Authentication (Giriş Sistemi İçin)
// ----------------------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Auth/Login"; // Giriş yapmayanlar buraya atılır
        options.LogoutPath = "/Admin/Auth/Logout";
        options.AccessDeniedPath = "/Home/Error";
        options.ExpireTimeSpan = TimeSpan.FromHours(2); // 2 Saat boşta kalırsa atar
    });

// ----------------------
// Session & Cache
// ----------------------
builder.Services.AddSession();

// ----------------------
// AutoMapper
// ----------------------
builder.Services.AddAutoMapper(typeof(ProfileProfile).Assembly);

// ----------------------
// Database
// ----------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ----------------------
// Dependency Injection (Servis Kayıtları)
// ----------------------
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<IVolunteerWorkService, VolunteerWorkService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEducationService, EducationService>();
builder.Services.AddScoped<IReferenceService, ReferenceService>();

// Ziyaretçi Takip Servisi
builder.Services.AddScoped<IVisitorService, VisitorService>();

// ----------------------
// MVC, Filters & FluentValidation
// ----------------------
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<VisitorTrackingFilter>();
});

// FluentValidation Ayarları
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(ProjectCreateDtoValidator).Assembly);

var app = builder.Build();

Console.WriteLine("Two Factor Key: " + TwoFactorKeyGenerator.GenerateSecretKey());

// ----------------------
// 📌 Middleware Pipeline (İstek Hattı - SIRALAMA ÇOK ÖNEMLİ!)
// ----------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// 📌 GÜVENLİK: HTTP Güvenlik Başlıkları (Security Headers)
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Frame-Options", "DENY"); // Clickjacking Koruması
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block"); // XSS Koruması
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff"); // MIME Koruması
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    await next();
});

app.UseRouting();

// 📌 Rate Limiter KESİNLİKLE UseRouting'den sonra olmalıdır!
app.UseRateLimiter();

app.UseSession();

// 📌 GÜVENLİK: Authentication (Kimlik Doğrulama) Authorization'dan ÖNCE gelmeli!
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets(); // .NET 9 Static Assets

// ----------------------
// Routing (Yönlendirmeler)
// ----------------------
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();