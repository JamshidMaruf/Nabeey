using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Assets;

namespace Nabeey.Domain.Entities.Contexts;

public class ContentVideo : Auditable
{
    public string Title { get; set; }
    public string Discription { get; set; }

	public long ContentId { get; set; }
    public Content Content { get; set; }

    public long? VideoId { get; set; }
    public Asset Video { get; set; }
}