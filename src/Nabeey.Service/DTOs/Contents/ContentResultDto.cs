using Nabeey.Service.DTOs.Books;
using Nabeey.Service.DTOs.Articles;
using Nabeey.Service.DTOs.ContentAudios;
using Nabeey.Service.DTOs.ContentVideos;
using Nabeey.Service.DTOs.ContentCategories;

namespace Nabeey.Service.DTOs.Contents;

public class ContentResultDto
{
	public long Id { get; set; }
	public ContentCategoryResultDto ContentCategory { get; set; }
	public ICollection<BookResultDto> Books { get; set; }
	public ICollection<ContentAudioResultDto> Audios { get; set; }
	public ICollection<ContentVideoResultDto> Videos { get; set; }
	public ICollection<ArticleResultDto> Articles { get; set; }
}