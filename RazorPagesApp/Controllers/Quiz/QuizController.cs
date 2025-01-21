using Core.Questions.QuestionServices;
using Core.Users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuiApp.WebMVC.Controllers.Quiz.Models;
using QuiApp.WebMVC.Controllers.Quizes.Models;
using System.Security.Claims;

namespace QuiApp.WebMVC.Controllers.Quizes;

[Route("Quiz")]
public class QuizController : Controller
{
    private readonly IQuestionRespository _questionRespository;
    private readonly IUserRepository _userRepository ;

    public QuizController(IQuestionRespository questionRespository, IUserRepository userRepository)
    {
        _questionRespository = questionRespository;
        _userRepository = userRepository;
    }

    [HttpGet("info")] // сюда можно класть какой пак вопросов будет проходить пользователь
    public IActionResult InfoView()
        => View("Info", new InfoData()
        {
            InfoMessage = "Этот квиз бла-бла.",
            InstructionMessage = "Делай хорошо, плохо не делай"
        });

    [HttpGet("")] // сюда можно класть какой пак вопросов будет проходить пользователь
    [Authorize(Roles = "User")]
    public async Task<IActionResult> QuizView()
    {
        var questions = await _questionRespository.GetQuestions();

        return View("Quiz", new QuizData()
        {
            Questions = questions
        });
    }

    [HttpGet("result")] // сюда можно класть какой пак вопросов будет проходить пользователь
    [Authorize(Roles = "User")]
    public async Task<IActionResult> ResultView([FromQuery]Guid id)
    {
        var questions = await _questionRespository.GetQuestions();

        var user = await _userRepository.GetUser(id);

        if (user == null)
            return RedirectToAction("LoginPage","Users");

        return View("Result", new ResultDto()
        {
            Questions = questions,
            User = user,
            Progress = user.Progress
        });
    }
}
