using Nabeey.Service.DTOs.Answer;
using Nabeey.Service.DTOs.Question;

namespace Nabeey.Service.DTOs.QuestionAnswer;

public class QuestionAnswerResultDto
{
    public long AnswerId { get; set; }
    public AnswerResultDto Answer { get; set; }
    public long QuestionId { get; set; }
    public QuestionResultDto Question { get; set; }
    public bool IsTrue { get; set; }
}
