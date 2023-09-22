using Nabeey.Domain.Commons;
using Nabeey.Domain.Contexts;

namespace Nabeey.Domain.Entities.Articles;

public class Article : Auditable
{
    public string Text { get; set; }
    public int ContentId { get; set; }
    public Content Content { get; set; }
}
