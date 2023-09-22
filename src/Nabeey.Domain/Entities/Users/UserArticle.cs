using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Articles;

namespace Nabeey.Domain.Entities.Users;

public class UserArticle
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long ArticleId { get; set; }
    public Article Article { get; set; }
}
