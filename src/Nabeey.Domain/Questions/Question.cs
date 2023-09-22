using Nabeey.Domain.Assets;
using Nabeey.Domain.Commons;

namespace Nabeey.Domain.Question;

public class Question:Auditable
{
    public long AssetId { get; set; }
    public Asset Asset { get; set; }
    public string Text { get; set; }
}
