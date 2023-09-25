using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.QuizQuestions;

namespace Nabeey.Domain.Entities.Quizzes;

public class Quiz : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int QuestionCount { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public long ContentCategoryId { get; set; }
    public ContentCategory ContentCategory { get; set; }
}