
using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.QuestionAnswers;
using Nabeey.Domain.Entities.QuizQuestions;
using Nabeey.Domain.Entities.Quizzes;

namespace Nabeey.Domain.Entities.Questions;

public class Question : Auditable
{
    public string Text { get; set; }

    public long? ImageId { get; set; }
    public Asset Image { get; set; }

    public ICollection<Answer> Answers { get; set; }
}