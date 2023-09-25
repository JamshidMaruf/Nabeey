
using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Assets;

namespace Nabeey.Domain.Entities.Questions;

public class Question : Auditable
{
	public string Text { get; set; }
	public long? FileId { get; set; }
	public Asset File { get; set; }
}
