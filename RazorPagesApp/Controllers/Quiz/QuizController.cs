using Core.Questions.QuestionServices;
using Core.Users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuiApp.WebMVC.Controllers.Quiz.Models;
using QuiApp.WebMVC.Controllers.Quizes.Models;

namespace QuiApp.WebMVC.Controllers.Quiz;

[Route("Quiz")]
public class QuizController(
    IQuestionRespository questionRepository, 
    IUserRepository userRepository) : Controller
{
    private readonly IQuestionRespository _questionRepository = questionRepository;
    private readonly IUserRepository _userRepository = userRepository;

    [HttpGet("info")] // сюда можно класть какой пак вопросов будет проходить пользователь
    public IActionResult InfoView()
        => View("Info", new InfoDataDto()
        {
            QuestionPackName = "Стандартный тест",
            InfoMessage = "Этот квиз бла-бла.",
            InstructionMessage = "Делай хорошо, плохо не делай"
        });

    [HttpGet("")] // сюда можно класть какой пак вопросов будет проходить пользователь
    [Authorize(Roles = "User")]
    public async Task<IActionResult> QuizView()
    {
        var questions = await _questionRepository.GetQuestions();

        return View(
            "Quiz",
            new QuizDataDto()
            {
                QuestionPackName = "Стандартный тест",
                Questions = questions
            });
    }

    [HttpGet("result")] // сюда можно класть какой пак вопросов будет проходить пользователь
    [Authorize(Roles = "User")]
    public async Task<IActionResult> ResultView([FromQuery] Guid id)
    {
        var questions = await _questionRepository.GetQuestions();

        var user = await _userRepository.GetUser(id);

        if (user == null)
            return RedirectToAction("LoginPage", "Users");

        return View("Result", new ResultDto()
        {
            Questions = questions,
            User = user,
            Progress = user.Progress
        });
    }
}
