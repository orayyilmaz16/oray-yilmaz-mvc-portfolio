using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
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
// 📌 YENİ: Serilog ve Middleware namespace'leri
using Serilog;
using OrayPortfolio.Web.Middlewares;

// ----------------------
// 📌 Serilog Başlangıç Kurulumu (Uygulama ayağa kalkmadan loglamayı başlatır)
// ----------------------
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning) // Microsoft'un gereksiz loglarını filtreler
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/orayportfolio-log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // 📌 YENİ: Serilog'u .NET Uygulamasına Bağlama
    builder.Host.UseSerilog();

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
            options.LoginPath = "/Admin/Auth/Login";
            options.LogoutPath = "/Admin/Auth/Logout";
            options.AccessDeniedPath = "/Home/Error";
            options.ExpireTimeSpan = TimeSpan.FromHours(2);
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
        context.Response.Headers.Append("X-Frame-Options", "DENY");
        context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        await next();
    });

    app.UseRouting();

    // 📌 YENİ: Serilog'un Kendi İstek Günlüğünü Aktif Etme (Routing'den hemen sonra)
    app.UseSerilogRequestLogging();

    // 📌 YENİ: Yazdığımız Özel Ziyaretçi Takip Aracını Sisteme Dahil Etme
    app.UseMiddleware<VisitorLoggingMiddleware>();

    app.UseRateLimiter();
    app.UseSession();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapStaticAssets(); // .NET 9/10 Static Assets

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
}
catch (Exception ex)
{
    // 📌 YENİ: Başlatma sırasında oluşan kritik hataları yakala
    Log.Fatal(ex, "Uygulama başlatılırken kritik bir hata oluştu!");
}
finally
{
    // 📌 YENİ: Uygulama kapanırken bellekte kalan son logları dosyaya yazdır ve kapat
    Log.CloseAndFlush();
}