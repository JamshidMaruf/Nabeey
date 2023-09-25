using Nabeey.Service.DTOs.Assets;

namespace Nabeey.Service.DTOs.Question;

public class QuestionResultDto
{
	public long Id { get; set; }
	public string Text { get; set; }
	public AssetResultDto File { get; set; }
}
