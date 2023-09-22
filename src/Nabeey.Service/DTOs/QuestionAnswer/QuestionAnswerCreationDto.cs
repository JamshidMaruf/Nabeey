namespace Nabeey.Service.DTOs.QuestionAnswer;

public class QuestionAnswerCreationDto
{
    public long AnswerId { get; set; }
    public long QuestionId { get; set; }
    public bool IsTrue { get; set; }
}
