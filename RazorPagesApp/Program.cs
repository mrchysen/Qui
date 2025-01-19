using Core.AdminAccess.Authorization;
using Core.Questions.QuestionServices;
using DAL;
using DAL.AdminAccessServices;
using DAL.QuestionsServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(op =>
    {
        op.LoginPath = "/";
    });

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("quiapp")));
builder.Services.AddScoped<IAdminRegistrationService, AdminRegistrationService>();
builder.Services.AddScoped<IQuestionRespository, QuestionsRepository>();

builder.Services.AddHttpContextAccessor();    // Enable use HttpContext in constructors
builder.Services.AddDistributedMemoryCache(); // Add caching
builder.Services.AddSession();                // Add sessions
builder.Services.AddRazorPages();             // Add razor pages in apllication

var app = builder.Build();

app.UseAuthentication();
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