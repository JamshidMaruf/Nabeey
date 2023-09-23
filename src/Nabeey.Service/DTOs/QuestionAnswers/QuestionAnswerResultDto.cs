using Nabeey.Service.DTOs.Answer;

namespace Nabeey.Service.DTOs.QuestionAnswers;

public class QuestionAnswerResultDto
{
    public long Id { get; set; }
    public AnswerResultDto Answer { get; set; }
    public QuestionAnswerResultDto Question { get; set; }
    public bool IsTrue { get; set; }
}
