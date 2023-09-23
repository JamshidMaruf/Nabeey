using Nabeey.Service.DTOs.Question;

namespace Nabeey.Service.DTOs.Quizzes.QuizQuestions;

public class QuizQuestionResultDto
{
    public long Id { get; set; }
    public QuizResultDto Quiz { get; set; }
    public QuestionResultDto Question { get; set; }
}
