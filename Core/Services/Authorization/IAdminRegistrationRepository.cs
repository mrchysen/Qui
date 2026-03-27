using Qui.Core.Models;

namespace Qui.Core.Services.Authorization;

public interface IAdminRegistrationRepository
{
    public bool IsAdmin(Registration registration);
}
