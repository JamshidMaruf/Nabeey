
using Nabeey.Domain.Answers;
using Nabeey.Domain.Commons;
using Nabeey.Domain.Questions;

namespace Nabeey.Domain.QuestionAnswers;

public class QuestionAnswer:Auditable
{
    public long AnswerId { get; set; }
    public Answer Answer { get; set; }
    public long QuestionId { get; set; }
    public Question Question { get; set; }
    public bool IsTrue { get; set; }
}
