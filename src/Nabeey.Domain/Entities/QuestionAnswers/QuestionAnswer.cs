using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;

namespace Nabeey.Domain.Entities.QuestionAnswers;

public class QuestionAnswer : Auditable
{
    public long AnswerId { get; set; }
    public Answer Answer { get; set; }

    public long QuestionId { get; set; }
    public Question Question { get; set; }

	public long UserId { get; set; }
	public User User { get; set; }

	public long QuizId { get; set; }
	public Quiz Quiz { get; set; }
}