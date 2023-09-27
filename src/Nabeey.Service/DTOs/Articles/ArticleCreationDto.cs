using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.Articles;

public class ArticleCreationDto
{
	public string Text { get; set; }
	public long CategoryId { get; set; }
	public long UserId { get; set; }
	public IFormFile Image { get; set; }
}