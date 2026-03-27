using Qui.Core.Services.Questions;
using System.ComponentModel.DataAnnotations;

namespace Qui.Core.Models;

public class UserProgress
{
    [Key]
    public Guid Id { get; set; }
    public int CurrentQuestion { get; set; } = 0;
    public List<string> Answers { get; set; } = new();
    public List<bool> WasSearched { get; set; } = new();
    public List<DateTime> AnswerStartDateTime { get; set; } = new();
    public List<DateTime> AnswerEndDateTime { get; set; } = new();
    public List<bool> IsRightAnswerList { get; set; } = new();

    public int RightAnswers { get; set; } = 0;

    public int CountRightAnswers(IQuestionHandler questions)
    {
        RightAnswers = 0;

        for (int i = 0; i < Answers.Count; i++)
        {
            if (questions[i].IsAnswer(Answers[i]))
            {
                RightAnswers += 1;
            }
        }

        return RightAnswers;
    }
}
