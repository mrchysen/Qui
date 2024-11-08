using Core.Models;

namespace Core.Services.Authorization;

public interface IAdminRegistration
{
    public bool IsAdmin(Registration registration);
}
