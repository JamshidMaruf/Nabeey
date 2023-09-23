using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Questions;

namespace Nabeey.Domain.Entities;

public class QuestionAnswer
{
    public long AnswerId { get; set; }
    public Answer Answer { get; set; }

    public long QuestionId { get; set; }
    public Question Question { get; set; }

    public bool IsTrue { get; set; }
}
