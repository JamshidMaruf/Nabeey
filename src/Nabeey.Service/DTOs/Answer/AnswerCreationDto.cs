using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.QuestionAnswers;

namespace Nabeey.Service.DTOs.Answer;

public class AnswerCreationDto
{
    public long AssetId { get; set; }
    public string Text { get; set; }
}
