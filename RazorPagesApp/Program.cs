using Core.Services.Authorization;
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("quiapp")));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAdminRegistration>(x => x.GetService<AppDbContext>());

builder.Services.AddHttpContextAccessor();    // Enable use HttpContext in constructors
builder.Services.AddDistributedMemoryCache(); // Add caching
builder.Services.AddSession();                // Add sessions
builder.Services.AddRazorPages();             // Add razor pages in apllication

var app = builder.Build();

app.UseAuthentication();     // Обязательно перед UseAuthorization!
app.UseAuthorization();
app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseSession();    // Use sessia
app.MapRazorPages(); // Routing for razor pages
app.MapControllers();

// Custom Middelwares \\
//app.UseMiddleware<AuntificationMiddelware>();
//app.UseMiddleware<AdministrationControlMiddelware>();

app.Run();