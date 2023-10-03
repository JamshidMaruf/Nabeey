using Nabeey.Service.DTOs.Quizzes;

namespace Nabeey.Domain.Entities.Quizzes;

public class ResultDto
{
	public int CorrectAnswers { get; set; }
	public int IncorrectAnswers { get; set; }
	public int QuestionCount { get; set; }
	public double Ball { get; set; }
	public QuizResultDto Quiz { get; set; }
}