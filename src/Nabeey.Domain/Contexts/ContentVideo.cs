using Nabeey.Domain.Assets;
using Nabeey.Domain.Commons;

namespace Nabeey.Domain.Contexts;

public class ContentVideo : Auditable
{
    public long ContentId { get; set; }
    public Content Content { get; set; }

    public long AssetId { get; set; }
    public Asset Asset { get; set; }
}