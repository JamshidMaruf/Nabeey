using Nabeey.Domain.Commons;

namespace Nabeey.Domain.Entities.Contexts;

public class Content : Auditable
{
    public long ContentCategoryId { get; set; }
    public ContentCategory ContentCategory { get; set; }
}