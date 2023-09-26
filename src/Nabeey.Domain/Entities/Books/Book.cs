using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Assets;

namespace Nabeey.Domain.Entities.Books;

public class Book : Auditable
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public long FileId { get; set; }
    public Asset File { get; set; }

    public long? ImageId { get; set; }
    public Asset Image { get; set; }

}