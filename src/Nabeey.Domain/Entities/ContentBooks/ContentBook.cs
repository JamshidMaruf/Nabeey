using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Contexts;

namespace Nabeey.Domain.Entities.ContentBooks;

public class ContentBook : Auditable
{
    public long BookId { get; set; }
    public Book Book { get; set; }

    public long ContentId { get; set; }
    public Content Content { get; set; }
}
