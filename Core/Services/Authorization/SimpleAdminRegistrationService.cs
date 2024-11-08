using Core.Models;

namespace Core.Services.Authorization;

public class SimpleAdminRegistrationService : IAdminRegistration
{
    protected List<Registration> Registrations = new List<Registration>();
    protected string FilePath { get; set; }

    public SimpleAdminRegistrationService(string filePath)
    {
        FilePath = filePath;

        if (!File.Exists(FilePath))
            throw new FileNotFoundException(FilePath);

        Registrations = File.ReadAllLines(FilePath).Select(elem =>
        {
            var data = elem.Split();

            return new Registration() { Login = data[0], Password = data[1] };
        }).ToList();
    }

    public bool IsAdmin(Registration registration)
    {
        return Registrations.Contains(registration);
    }
}
