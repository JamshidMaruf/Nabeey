using Nabeey.Service.DTOs.Articles;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.DTOs.ContentAudios;
using Nabeey.Service.DTOs.ContentVideos;

namespace Nabeey.Service.DTOs.ContentCategories;

public class ContentCategoryResultDto
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public AssetResultDto Image { get; set; }
	public ICollection<BookResultDto> Books { get; set; }
	public ICollection<ContentAudioResultDto> Audios { get; set; }
	public ICollection<ContentVideoResultDto> Videos { get; set; }
	public ICollection<ArticleResultDto> Articles { get; set; }
}
