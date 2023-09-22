namespace Nabeey.Service.DTOs.QuestionAnswer;

public class QuestionAnswerUpdateDto
{
    public long Id { get; set; }
    public long AnswerId { get; set; }
    public long QuestionId { get; set; }
    public bool IsTrue { get; set; }
}
