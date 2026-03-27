using Qui.Core.Models;
namespace Qui.Core.Services.Questions;
public class QuestionService : IQuestionHandler
{
    protected List<Question> Questions { get; set; } = new();
    protected IQuestionRespository Bd { get; set; }

    public int Count => Questions.Count;
    public Question this[int index] => Questions[index];

    public QuestionService(IQuestionRespository bd)
    {
        Questions = bd.GetQuestions();
        Bd = bd;
    }

    public void ChangeQuestion(int index, Question question)
    {
        if (Questions.Count <= index)
            return;

        question.Order = index + 1;
        Questions[index] = question;

        Save();
    }

    public void CreateQuestion(Question question)
    {
        if(Count == 0)
            question.Order = Count + 1;
        else
            question.Order = Questions.Last().Order + 1;

        Questions.Add(question);

        Save();
    }

    public void DeleteQuestion(int index)
    {
        if (Questions.Count <= index)
            return;

        Questions.RemoveAt(index);

        Save();
    }

    public void DeleteAllQuestions()
    {
        Questions.Clear();

        Save();
    }

    protected void Save()
    {
        Bd.SaveQuestions(Questions);
    }
}
