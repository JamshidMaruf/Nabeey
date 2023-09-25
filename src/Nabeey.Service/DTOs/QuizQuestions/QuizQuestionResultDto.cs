using Nabeey.Service.DTOs.Question;
using Nabeey.Domain.Entities.Quizzes;

namespace Nabeey.Service.DTOs.QuizQuestions;

public class QuizQuestionResultDto
{
    public long Id { get; set; }
    public ResultDto Quiz { get; set; }
    public QuestionResultDto Question { get; set; }
}
