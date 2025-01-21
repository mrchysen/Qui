using Core.Questions;
using Core.Questions.QuestionServices;
using System.ComponentModel.DataAnnotations;

namespace Core.UserProgressFeatures;

public class UserProgress
{
    [Key]
    public Guid Id { get; set; }
    public List<string> Answers { get; set; } = new();
    public List<bool> WasSearched { get; set; } = new();
    public List<DateTime> AnswerStartDateTime { get; set; } = new();
    public List<DateTime> AnswerEndDateTime { get; set; } = new();
    public List<bool> RightAnswerList { get; set; } = new();
    public int RightAnswers { get; set; } = 0;

    public int CountRightAnswers(List<Question> questions)
    {
        RightAnswers = 0;

        for (int i = 0; i < Answers.Count; i++)
        {
            if (questions[i].IsAnswer(Answers[i]))
            {
                RightAnswerList.Add(true);
                RightAnswers += 1;
            }
            RightAnswerList.Add(false);
        }

        return RightAnswers;
    }
}
