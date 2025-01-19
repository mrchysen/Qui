using System.ComponentModel.DataAnnotations;

namespace Core.Questions;

public class Question
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public List<string> Answers { get; set; } = new List<string>();
    public int Order { get; set; } = 0;

    public bool IsAnswer(string answer)
    {
        answer = answer.Trim().ToLower();

        return Answers.Contains(answer);
    }
}
