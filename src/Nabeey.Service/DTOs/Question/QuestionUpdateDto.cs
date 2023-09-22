using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.QuestionAnswers;

namespace Nabeey.Service.DTOs.Question;

public class QuestionUpdateDto
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public string Text { get; set; }
}
