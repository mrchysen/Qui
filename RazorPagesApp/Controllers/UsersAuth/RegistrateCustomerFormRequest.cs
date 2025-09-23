using Core.Models;

namespace RazorPagesApp.Controllers.UsersAuth;

public class RegistrateCustomerFormRequest
{
    public string Name { get; set; } = null!;
    public string Sername { get; set; } = null!;
    public string FatherName { get; set; } = null!;
    public Sex Sex { get; set; }
    public int Age { get; set; }
}