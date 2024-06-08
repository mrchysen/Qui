using RazorPagesApp.Models;

namespace RazorPagesApp.Services.Authorization;

public interface IAdminRegistration
{
    public bool IsAdmin(Registration registration);
}
