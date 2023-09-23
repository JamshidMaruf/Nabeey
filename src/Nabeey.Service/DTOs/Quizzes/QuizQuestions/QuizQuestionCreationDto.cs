using Nabeey.Domain.Entities.Quizzes;

namespace Nabeey.Service.DTOs.Quizzes.QuizQuestions;

public class QuizQuestionCreationDto
{
    public long QuizId { get; set; }
    public long QuestionId { get; set; }
}