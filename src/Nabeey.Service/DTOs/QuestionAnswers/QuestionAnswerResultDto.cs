using Nabeey.Service.DTOs.Answer;
using Nabeey.Service.DTOs.Answers;
using Nabeey.Service.DTOs.Question;
using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Service.DTOs.QuestionAnswers;

public class QuestionAnswerResultDto
{
	public AnswerResultDto Answer { get; set; }
	public QuestionResultDto Question { get; set; }
	public QuizResultDto Quiz { get; set; }
	public UserResultDto User { get; set; }
}
