
using Nabeey.Domain.Commons;

namespace Nabeey.Domain.QuestionAnswer;

public class QuestionAnswer:Auditable
{
    public long AnswerId { get; set; }
    public Answer Answer { get; set; }
    public long QuestionId { get; set; }
}
