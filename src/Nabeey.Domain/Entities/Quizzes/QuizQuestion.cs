using Nabeey.Domain.Commons;
using Nabeey.Domain.Questions;

namespace Nabeey.Domain.Entities.Quizzes;

public class QuizQuestion : Auditable
{
    public long QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public ICollection<Question> Questions { get; set; }
}
