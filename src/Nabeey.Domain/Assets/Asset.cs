using Nabeey.Domain.Commons;

namespace Nabeey.Domain.Assets;

public class Asset : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
}