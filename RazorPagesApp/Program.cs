using Core.Services.Authorization;
using Core.Services.Questions;
using EFLearning;
using RazorPagesApp.Middelwares;
using RazorPagesApp.Services.Progress;
using RazorPagesApp.Services.Questions;

var builder = WebApplication.CreateBuilder(args);

var bd = new AppDbContext(builder.Configuration);
var questionHandler = new QuestionService(bd);

builder.Services.AddSingleton<IQuestionHandler>(questionHandler);
builder.Services.AddSingleton<IAdminRegistration>(bd);
builder.Services.AddSingleton<IUserCRUD>(bd);
builder.Services.AddSingleton<IQuestionsBD>(bd);

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