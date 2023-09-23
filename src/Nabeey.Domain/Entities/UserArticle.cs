using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Users;

namespace Nabeey.Domain.Entities;

public class UserArticle
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long ArticleId { get; set; }
    public Article Article { get; set; }
}
