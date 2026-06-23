using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
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

var builder = WebApplication.CreateBuilder(args);

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

// 📌 Ziyaretçi Takip Servisi
builder.Services.AddScoped<IVisitorService, VisitorService>();

// ----------------------
// MVC, Filters & FluentValidation
// ----------------------
// 📌 ÇAKIŞMA ÇÖZÜLDÜ: Hem MVC aktif edildi, hem de içine Filtre yerleştirildi!
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
// Middleware Pipeline (İstek Hattı)
// ----------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
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