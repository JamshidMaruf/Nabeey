using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.DTOs.Quizzes;

namespace Nabeey.Service.DTOs.QuizQuestions;

public class QuizQuestionResultDto
{
	public long Id { get; set; }
	public QuizResultDto Quiz { get; set; }
	public QuestionResultDto Question { get; set; }

}