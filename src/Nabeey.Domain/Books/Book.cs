using Nabeey.Domain.Assets;
using Nabeey.Domain.Commons;

namespace Nabeey.Domain.Books;

public class Book : Auditable
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public long AssetId { get; set; }
    public Asset Asset { get; set; }
    public string Text { get; set; }
}
