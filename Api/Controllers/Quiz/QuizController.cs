using Microsoft.AspNetCore.Mvc;

namespace RazorPagesApp.Controllers.Quiz;

[Route("Quiz")]
public class QuizController : Controller
{
    [Route("")]
    public IActionResult Index() => View("Quiz");
}
