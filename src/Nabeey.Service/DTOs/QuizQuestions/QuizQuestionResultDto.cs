using Nabeey.Service.DTOs.Questions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Service.DTOs.Answers;
using Nabeey.Service.DTOs.Quizzes;

namespace Nabeey.Service.DTOs.QuizQuestions;

public class QuizQuestionResultDto
{
    public long Id { get; set; }
    public QuizResultDto Quiz { get; set; }
    public QuestionResultDto Question { get; set; }

    public ICollection<AnswerResultDto> Answers { get; set; }
}