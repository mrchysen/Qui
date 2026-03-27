using Qui.Core.Models;
using Qui.Core.Services.Authorization;

namespace Qui.DAL.Repositories;

public class AdminRegistrationRepository(AppDbContext dbContext) : IAdminRegistrationRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public bool IsAdmin(Registration registration)
    {
        return _dbContext.Registrations.ToList().Contains(registration);
    }
}