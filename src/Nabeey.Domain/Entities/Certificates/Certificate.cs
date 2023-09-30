using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;

namespace Nabeey.Domain.Entities.Certificates;

public class Certificate : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long FileId { get; set; }
    public Asset File { get; set; }
    public long QuizId { get; set; }
    public Quiz Quiz { get; set; }
}
