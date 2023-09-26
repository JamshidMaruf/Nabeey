using Nabeey.Service.DTOs.Quizzes;

namespace Nabeey.Domain.Entities.Quizzes;

public class ResultDto
{
	public int CorrectAnswers { get; set; }
	public int IncorrectAnswers { get; set; }
	public double Percentage { get; set; }
	public QuizResultDto Quiz { get; set; }
}
