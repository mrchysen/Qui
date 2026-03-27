using Qui.Core.Models;

namespace Qui.Core.Services.Authorization;

public interface IAdminRegistration
{
    public bool IsAdmin(Registration registration);
}
