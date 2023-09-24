using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Users;

namespace Nabeey.Domain.Entities.Articles;

public class UserArticle : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long ArticleId { get; set; }
    public Article Article { get; set; }
}