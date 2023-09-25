using Nabeey.Domain.Enums;
using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Articles;

namespace Nabeey.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PasswordHash { get; set; }
    public Role UserRole { get; set; } = Role.User;

    public long? ImageId { get; set; }
    public Asset Image { get; set; }
}
