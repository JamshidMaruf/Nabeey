using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Questions;

namespace Nabeey.Service.DTOs.Answers;

public class AnswerResultDto
{
	public long Id { get; set; }
	public string Text { get; set; }
	public AssetResultDto Asset { get; set; }
	public QuestionResultDto Question { get; set; }
}