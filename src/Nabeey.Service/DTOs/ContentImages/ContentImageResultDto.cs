using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Contexts;

namespace Nabeey.Service.DTOs.ContentImages;

public class ContentImageResultDto
{
    public Content ContentId { get; set; }
    public Asset AssetId { get; set; }
}
