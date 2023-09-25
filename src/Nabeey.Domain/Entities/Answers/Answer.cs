using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Questions;

namespace Nabeey.Domain.Entities.Answers;

public class Answer : Auditable
{
	public string Text { get; set; }
	public long? AssetId { get; set; }
	public Asset Asset { get; set; }
	public long QuestionId { get; set; }
	public Question Question { get; set; }
}
