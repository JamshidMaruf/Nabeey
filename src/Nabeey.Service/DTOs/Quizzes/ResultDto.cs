namespace Nabeey.Domain.Entities.Quizzes;

public class ResultDto
{
	public int CorrectAnswers { get; set; }
	public int IncorrectAnswers { get; set; }
	public double Percentage { get; set; }
	public ResultDto Quiz { get; set; }
}
