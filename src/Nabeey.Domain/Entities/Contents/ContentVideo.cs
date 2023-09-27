using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Assets;
using System.Security.Cryptography;

namespace Nabeey.Domain.Entities.Contexts;

public class ContentVideo : Auditable
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string VideoLink { get; set; }
    public long ContentId { get; set; }
    public Content Content { get; set; }
}