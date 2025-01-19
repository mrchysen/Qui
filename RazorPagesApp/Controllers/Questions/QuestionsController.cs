using Core.Questions;
using Core.Questions.QuestionServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuiApp.WebMVC.Controllers.Questions.Models;

namespace RazorPagesApp.Controllers.Questions;

[Route("questions")]
[Authorize(Roles = "Admin")]
public class QuestionsController : Controller
{
    private readonly IQuestionRespository _questionRespository;

    public QuestionsController(IQuestionRespository questionRespository)
    {
        _questionRespository = questionRespository;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetList() => View("List", new QuestionsListDto()
    {
        Questions = await _questionRespository.GetQuestions()
    });

    [HttpGet("change")]
    public async Task<IActionResult> ChangeView([FromQuery] Guid id)
    {
        var question = (await _questionRespository.GetByFilter(x => x.Id == id)).FirstOrDefault();

        if (question is null) 
        {
            return RedirectToAction("GetList");
        }

        return View("ChangeQuestion", new QuestionDto()
        {
            Text = question.Text,
            Answers = string.Join("\n", question.Answers),
            Id = id
        });
    }

    [HttpPost("change")]
    public async Task<IActionResult> Change([FromForm] QuestionDto questionDto)
    {
        var question = (await _questionRespository.GetByFilter(x => x.Id == questionDto.Id)).FirstOrDefault();

        if (question is null)
            return RedirectToAction("GetList");

        question.Text = questionDto.Text;
        question.Answers = questionDto.Answers.Split("\r\n").ToList();

        _questionRespository.Update(question);

        return RedirectToAction("GetList");
    }

    [HttpGet("create")]
    public IActionResult CreateView() => View("CreateQuestion");

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromForm] QuestionDto questionDto)
    {
        await _questionRespository.AddQuestion(new Question() 
        { 
            Id = Guid.NewGuid(),
            Text = questionDto.Text,
            Answers = questionDto.Answers.Split("\r\n").ToList()
        });

        return RedirectToAction("GetList");
    }

    [HttpGet("delete")]
    public IActionResult Delete([FromQuery] Guid id)
    {
        _questionRespository.DeleteQuestion(id);

        return RedirectToAction("GetList");
    }
}
