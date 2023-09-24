using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.QuestionAnswers;

namespace Nabeey.Service.DTOs.Question;

public class QuestionResultDto
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public AssetResultDto Asset { get; set; }
    public string Text { get; set; }
    public ICollection<QuestionAnswerResultDto> QuestionAnswers { get; set; }
}
