using Microsoft.AspNetCore.Mvc;

namespace Qui.Api.Controllers.Quiz;

[Route("Quiz")]
public class QuizController : Controller
{
    [Route("")]
    public IActionResult Index() => View("Quiz");
}
