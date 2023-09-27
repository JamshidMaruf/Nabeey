using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.Users;

namespace Nabeey.Domain.Entities.Articles;

public class Article : Auditable
{
    public string Text { get; set; }
    public long ContentId { get; set; }
    public Content Content { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long? ImageId { get; set; }
    public Asset Image { get; set; }
}