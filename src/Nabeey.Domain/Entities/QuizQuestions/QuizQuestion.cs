using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.Quizzes;

namespace Nabeey.Domain.Entities.QuizQuestions;

public class QuizQuestion : Auditable
{
    public long QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public long QuestionId { get; set; }
    public Question Question { get; set; }
}