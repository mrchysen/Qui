using System.ComponentModel.DataAnnotations;

namespace RazorPagesApp.Models;

public class Registration
{
    [Key]
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public override bool Equals(object? obj)
    {
        if(obj is  Registration r) 
            return r.Login == Login && r.Password == Password;
        return base.Equals(obj);
    }
}
