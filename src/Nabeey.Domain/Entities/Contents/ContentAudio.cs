using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Assets;

namespace Nabeey.Domain.Entities.Contexts;

public class ContentAudio : Auditable
{
	public string Title { get; set; }
	public string Description { get; set; }

	public long CategoryId { get; set; }
	public ContentCategory Category { get; set; }

	public long AudioId { get; set; }
	public Asset Audio { get; set; }
}