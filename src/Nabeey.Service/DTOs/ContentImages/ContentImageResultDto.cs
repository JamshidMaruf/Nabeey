using Nabeey.Domain.Contexts;
using Nabeey.Domain.Entities.Assets;

namespace Nabeey.Service.DTOs.ContentImages;

public class ContentImageResultDto
{
	public Content ContentId { get; set; }
	public Asset AssetId { get; set; }
}
