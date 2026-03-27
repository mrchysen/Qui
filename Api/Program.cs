using Microsoft.EntityFrameworkCore;
using Qui.Api.Middelwares;
using Qui.Core.Services.Authorization;
using Qui.Core.Services.Progress;
using Qui.Core.Services.Questions;
using Qui.DAL;
using Qui.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite());

builder.Services.AddScoped<IQuestionRespository, QuestionRespository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminRegistrationRepository, AdminRegistrationRepository>();

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