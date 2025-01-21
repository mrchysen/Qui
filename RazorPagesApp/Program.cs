using Core.AdminAccess.Authorization;
using Core.Questions.QuestionServices;
using Core.Users.Services;
using DAL;
using DAL.AdminAccessServices;
using DAL.QuestionsServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QuiApp.DAL.UserProgressServices;

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
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();
app.MapBlazorHub();

app.Run();