namespace Nabeey.Service.DTOs.QuestionAnswers;

public class QuestionAnswerCreationDto
{
	public long AnswerId { get; set; }
	public long QuestionId { get; set; }
    public long UserId { get; set; }
    public long QuizId { get; set; }
}