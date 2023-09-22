namespace Nabeey.Domain.Contexts;

public class ContentImage
{
    public long ContentId { get; set; }
    public Content Content { get; set; }

    public long AssetId { get; set; }
    public Asset Asset { get; set; }
}
