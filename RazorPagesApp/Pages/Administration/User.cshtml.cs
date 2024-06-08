using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Models;
using RazorPagesApp.Services.Progress;
using System.Globalization;

namespace RazorPagesApp.Pages.Administration
{
    public class UserModel : PageModel
    {
        protected IUserCRUD UserHandler;
        public User User;
        public UserModel(IUserCRUD userDb) 
        { 
            UserHandler = userDb;
        }
        public IActionResult OnGet(Guid id)
        {
            User = UserHandler.GetUser(id);

            return Page();
        }

        public bool IndexCondition(int index)
        {
            return index < User.Progress.Answers.Count &&
                index < User.Progress.IsRightAnswerList.Count &&
                index < User.Progress.AnswerEndDateTime.Count &&
                index < User.Progress.AnswerStartDateTime.Count &&
                index < User.Progress.WasSearched.Count;
        }
        public string GetStartDate(User user)
        {
            if (user.Progress.AnswerStartDateTime.Count <= 0)
            {
                return "нет";
            }
            return user.Progress.AnswerStartDateTime[0].ToLongDateString();
        }
    }
}
