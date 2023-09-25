using Nabeey.Domain.Entities.Assets;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Question;
using Nabeey.Service.DTOs.QuestionAnswers;

namespace Nabeey.Service.DTOs.Answer;

public class AnswerResultDto
{
    public long Id { get; set; }
	public string Text { get; set; }
	public AssetResultDto Asset { get; set; }
	public QuestionResultDto Question { get; set; }
}