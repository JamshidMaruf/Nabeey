using Nabeey.Service.DTOs.Answers;
using Nabeey.Service.DTOs.Assets;

namespace Nabeey.Service.DTOs.Questions;

public class QuestionResultDto
{
	public long Id { get; set; }
	public string Text { get; set; }
	public AssetResultDto Image { get; set; }
	public ICollection<AnswerResultDto> Answers { get; set; }
}