using Nabeey.Domain.Assets;
using Nabeey.Domain.Commons;

namespace Nabeey.Domain.Answer;

public class Answer:Auditable
{
    public long AssetId { get; set; }
    public Asset Asset { get; set; }
    public string Text { get; set; }
}
