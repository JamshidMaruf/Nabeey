using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.ContentCategories;

namespace Nabeey.Service.DTOs.Articles;

public class ArticleResultDto
{
	public long Id { get; set; }
	public string Text { get; set; }
	public ContentCategoryResultDto Category { get; set; }
	public AssetResultDto Image { get; set; }
}
