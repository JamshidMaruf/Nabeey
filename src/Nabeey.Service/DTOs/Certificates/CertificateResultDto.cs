using Nabeey.Service.DTOs.Assets;

namespace Nabeey.Service.DTOs.Certificates;

public class CertificateResultDto
{
    public long Id { get; set; }
    public AssetResultDto File { get; set; }
}
