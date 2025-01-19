using Core.AdminAccess;
using Core.AdminAccess.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DAL.AdminAccessServices;

public class AdminRegistrationService : IAdminRegistrationService
{
    private readonly AppDbContext _dbContext;

    public AdminRegistrationService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAdmin(AdminRegistration adminRegistration)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsAdmin(AdminRegistration adminRegistration)
        => await _dbContext.Registrations.Where(ad => ad.Password == adminRegistration.Password &&
            ad.Login == adminRegistration.Login).AnyAsync();
    

    public Task RemoveAdmin(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAdmin(AdminRegistration adminRegistration)
    {
        throw new NotImplementedException();
    }
}
