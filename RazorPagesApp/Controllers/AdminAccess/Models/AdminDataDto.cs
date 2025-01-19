namespace RazorPagesApp.Controllers.AdminAccess.Models;

public class AdminDataDto
{
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? Warning { get; set; }
}
