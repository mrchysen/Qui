namespace Core.AdminAccess.Authorization;

public interface IAdminRegistrationService
{
    Task<bool> IsAdmin(AdminRegistration adminRegistration);

    Task AddAdmin(AdminRegistration adminRegistration);

    Task RemoveAdmin(Guid id);

    Task UpdateAdmin(AdminRegistration adminRegistration);
}
