using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Users;

namespace Nabeey.Domain.Entities.Quizzes;

public class QuizResult : Auditable
{
    public long QuizId { get; set; }
    public Quiz Quiz { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public double Ball { get; set; }
    public int CorrectAnswerCount { get; set; }
    public int IncorrectAnswerCount { get; set; }
}
