using EFLearning;
using RazorPagesApp.Middelwares;
using RazorPagesApp.Models;
using RazorPagesApp.Services.Authorization;
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
builder.Services.AddRazorPages();             // Add razor pages in apllication

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseSession();    // Use sessia
app.MapRazorPages(); // Routing for razor pages

// Custom Middelwares \\
app.UseMiddleware<AuntificationMiddelware>();
app.UseMiddleware<AdministrationControlMiddelware>();

try
{
    app.Run();  
}
catch(Exception ex)
{
    string path = AppDomain.CurrentDomain.BaseDirectory;
    File.AppendAllText(Path.Combine(path, "error.log"),$"{ex.Message}\n{ex.StackTrace}");
}