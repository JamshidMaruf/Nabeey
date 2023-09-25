
using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.QuestionAnswers;
using Nabeey.Domain.Entities.QuizQuestions;
using Nabeey.Domain.Entities.Quizzes;

namespace Nabeey.Domain.Entities.Questions;

public class Question : Auditable
{
    public string Text { get; set; }

    public long? AssetId { get; set; }
    public Asset Asset { get; set; }

    public IEnumerable<QuizQuestion> QuizQuestions { get; set; }
    public IEnumerable<QuestionAnswer> QuestionAnswers { get; set; }
}
