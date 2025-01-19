using Core.AdminAccess;
using Core.AdminAccess.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages.Administration
{
    public class AdminAuthorizeModel : PageModel
    {
        protected IAdminRegistrationService Registrator { get; set; }
        public string Info { get; set; } = "";

        public AdminAuthorizeModel(IAdminRegistrationService registrator) 
        {
            Registrator = registrator;
        }

        public async Task<IActionResult> OnPost(AdminRegistration registration)
        {
            if (await Registrator.IsAdmin(registration))
            {
                HttpContext.Session.SetString("authentication", "admin");

                return Redirect("/Administration/Questions");
            }

            Info = "Неверный пароль или логин";

            return Page();
        }
    }
}
