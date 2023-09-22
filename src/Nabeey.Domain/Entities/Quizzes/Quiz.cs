using Nabeey.Domain.Commons;

namespace Nabeey.Domain.Entities.Quizzes;

public class Quiz : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int QuestionCount { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
