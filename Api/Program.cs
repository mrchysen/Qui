using Core.Services.Authorization;
using Core.Services.Questions;
using Microsoft.EntityFrameworkCore;
using Qui.Api.Middelwares;
using Qui.DAL;
using RazorPagesApp.Services.Progress;
using RazorPagesApp.Services.Questions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite());

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddHttpContextAccessor();    // Enable use HttpContext in constructors
builder.Services.AddDistributedMemoryCache(); // Add caching
builder.Services.AddSession();                // Add sessions
builder.Services.AddControllersWithViews();   // Add controllers pages in application

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseSession();    // Use sessia
app.MapControllers(); // Routing for razor pages

// Custom Middelwares \\
//app.UseMiddleware<AuntificationMiddelware>();
app.UseMiddleware<AdministrationControlMiddelware>();

app.Run();  