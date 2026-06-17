using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrayPortfolio.Application.Interfaces.Repositories;
using OrayPortfolio.Application.Interfaces.Services;
using OrayPortfolio.Application.Mapping;
using OrayPortfolio.Application.Services;
using OrayPortfolio.Infrastructure.Context;
using OrayPortfolio.Infrastructure.Repositories;
using OrayPortfolio.Infrastructure.UnitOfWork;
using OrayPortfolio.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();


// ----------------------
// AutoMapper (TEK SATIR)
// ----------------------
builder.Services.AddAutoMapper(typeof(ProfileProfile).Assembly);

// ----------------------
// Database
// ----------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ----------------------
// Dependency Injection
// ----------------------
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<IVolunteerWorkService, VolunteerWorkService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IMediaService, MediaService>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

Console.WriteLine("Two Factor Key: " + TwoFactorKeyGenerator.GenerateSecretKey());

// ----------------------
// Middleware
// ----------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

// ----------------------
// Routing
// ----------------------
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseSession();

app.Run();


