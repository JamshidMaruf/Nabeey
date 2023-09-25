using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Contents;

namespace Nabeey.Service.DTOs.Articles;

public class ArticleResultDto
{
    public long Id { get; set; }
	public string Text { get; set; }
	public ContentResultDto Content { get; set; }
	public AssetResultDto Image { get; set; }
}
