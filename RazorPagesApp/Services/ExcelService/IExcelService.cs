using RazorPagesApp.Models;

namespace RazorPagesApp.Services.ExcelService;

public interface IExcelService
{
    public bool CreateExcelFile(List<User> users, string path);

}
