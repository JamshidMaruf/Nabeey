namespace Nabeey.Service.DTOs.QuestionAnswers;

public class QuestionAnswerUpdateDto
{
	public long Id { get; set; }
	public long AnswerId { get; set; }
	public long QuestionId { get; set; }
	public long QuizId { get; set; }
    public long UserId { get; set; }
}
