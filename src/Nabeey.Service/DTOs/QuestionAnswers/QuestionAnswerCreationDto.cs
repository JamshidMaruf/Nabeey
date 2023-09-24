namespace Nabeey.Service.DTOs.QuestionAnswers;

public class QuestionAnswerCreationDto
{
    public long AnswerId { get; set; }
    public long QuestionId { get; set; }
    public bool IsTrue { get; set; }
}
