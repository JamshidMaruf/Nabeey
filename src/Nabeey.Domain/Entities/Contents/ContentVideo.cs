using Nabeey.Domain.Commons;

namespace Nabeey.Domain.Entities.Contexts;

public class ContentVideo : Auditable
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string VideoLink { get; set; }
	public long CategoryId { get; set; }
	public ContentCategory Category { get; set; }
}