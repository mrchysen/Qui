using Core.Users;

namespace QuiApp.WebMVC.Controllers.Users.Models;

public class UserLoginDto
{
    public string Name { get; set; } = string.Empty;
    public string Sername { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public Sex Sex { get; set; }
    public int Age { get; set; }
}
