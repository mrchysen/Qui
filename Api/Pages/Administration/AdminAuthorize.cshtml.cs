using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Qui.Core.Models;
using Qui.Core.Services.Authorization;

namespace Qui.Api.Pages.Administration
{
    public class AdminAuthorizeModel : PageModel
    {
        protected IAdminRegistrationRepository Registrator { get; set; }
        public string Info { get; set; } = "";

        public AdminAuthorizeModel(IAdminRegistrationRepository registrator) 
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
