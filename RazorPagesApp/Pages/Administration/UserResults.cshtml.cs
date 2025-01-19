using Application.ExcelService;
using Core.Questions.QuestionServices;
using Core.Users;
using Core.Users.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace RazorPagesApp.Pages.Administration;

public class UserResultsModel : PageModel
{
    public IUserRepository UserHandler;
    public List<User> Users;

    public UserResultsModel(IUserRepository users, IQuestionHandler questions) 
    {
        UserHandler = users;
        Users = UserHandler.GetUsers();
        Users.Sort((u1, u2) => - u1.GetStartTime().CompareTo(u2.GetStartTime()));
    }

    public IActionResult OnGetDeleteUser(Guid id)
    {
        UserHandler.DeleteUser(id);

        return Redirect("/Administration/UserResults");
    }
    public IActionResult OnGetClear()
    {
        UserHandler.DeleteAllUser();

        return Redirect("/Administration/UserResults");
    }

    /// <summary>
    /// Sending excel file with user`s results
    /// </summary>
    /// <returns></returns>
    public IActionResult OnGetExcelFile()
    {
        IExcelService excelGen = new ExcelGenerator();

        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "results.xlsx");
        
        excelGen.CreateExcelFile(UserHandler.GetUsers() , path);
        Console.WriteLine("װאיכ סמחהאם");
        //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "results.xlsx")

        return PhysicalFile(path, "application/excel", "results.xlsx");
    }

    public string GetStartDate(User user)
    {
        if(user.Progress.AnswerStartDateTime.Count <= 0)
        {
            return "";
        }
        return user.Progress.AnswerStartDateTime[0].ToString(new CultureInfo("de-DE"));
    }
}
