using RazorPagesApp.Models;

namespace RazorPagesApp.Middelwares;

public class ZeroQuestionControllerMiddelware
{
    protected readonly RequestDelegate next;

    public ZeroQuestionControllerMiddelware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, List<Question> questions)
    {
        if(questions.Count == 0 && context.Session.GetString("authentication") == "end" && context.Request.Path != "/info")
        {
            context.Response.Redirect("/info?info=Questions+not+found");
        }
        else
        {
            await next.Invoke(context);
        }
    }
}
