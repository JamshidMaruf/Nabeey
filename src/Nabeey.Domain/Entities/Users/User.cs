using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Enums;

namespace Nabeey.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PasswordHash { get; set; } = default!;
    public Role UserRole { get; set; } = Role.User;

    public long? AssetId { get; set; }
    public Asset Asset { get; set; }

    public IEnumerable<UserArticle> UserArticles { get; set; }
    public ICollection<Quiz> Quizzes { get; set; }
}
