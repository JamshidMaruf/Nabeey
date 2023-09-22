using Nabeey.Domain.Commons;

namespace Nabeey.Domain.Entities.Assets;

public class Asset : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
}