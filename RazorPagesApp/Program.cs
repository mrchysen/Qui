using Core.Services.Authorization;
using Core.Services.Questions;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Middelwares;
using RazorPagesApp.Services.Progress;
using RazorPagesApp.Services.Questions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite());
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthentication()
    .AddCookie(opt =>
    {
        opt.LoginPath = "/users/registrate-form";
    })
    .AddJwtBearer(options =>
    {


        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["token"];

                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();    // Enable use HttpContext in constructors
builder.Services.AddDistributedMemoryCache(); // Add caching
builder.Services.AddSession();                // Add sessions
builder.Services.AddControllersWithViews();   // Add controllers pages in application

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseSession();
app.MapControllers(); 

app.Run();  