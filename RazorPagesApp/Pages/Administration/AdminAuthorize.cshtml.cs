using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Models;
using RazorPagesApp.Services.Authorization;

namespace RazorPagesApp.Pages.Administration
{
    public class AdminAuthorizeModel : PageModel
    {
        protected IAdminRegistration Registrator { get; set; }
        public string Info { get; set; } = "";

        public AdminAuthorizeModel(IAdminRegistration registrator) 
        {
            Registrator = registrator;
        }

        public IActionResult OnPost(Registration registration) 
        {
            if (Registrator.IsAdmin(registration))
            {
                HttpContext.Session.SetString("authentication", "admin");

                return Redirect("/Administration/Questions");
            }

            Info = "Неверный пароль или логин";

            return Page();
        }
    }
}
