using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.Articles;

public class ArticleCreationDto
{
	public string Text { get; set; }
	public long ContentId { get; set; }
	public IFormFile? Image { get; set; }
}