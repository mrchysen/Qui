using System.ComponentModel.DataAnnotations;

namespace Core.AdminAccess;

public class AdminRegistration
{
    [Key]
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is AdminRegistration r)
            return r.Login == Login && r.Password == Password;
        return base.Equals(obj);
    }

    public override int GetHashCode()
        => Login.GetHashCode() ^ Password.GetHashCode();
}
