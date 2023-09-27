using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Articles;

namespace Nabeey.Domain.Entities.Assets;

public class Asset : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
}