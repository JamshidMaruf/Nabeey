using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Assets;

namespace Nabeey.Domain.Entities.Contexts;

public class ContentAudio : Auditable
{
    public long ContentId { get; set; }
    public Content Content { get; set; }

    public long AssetId { get; set; }
    public Asset Asset { get; set; }
}