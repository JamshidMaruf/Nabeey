using Nabeey.Domain.Commons;
using Nabeey.Domain.Contexts;

namespace Nabeey.Domain.Books;

public class ContentBook : Auditable
{
    public long BookId { get; set; }
    public Book Book { get; set; }

    public long ContentId { get; set; }
    public Content Content { get; set; }
}
