using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.Articles;

public class ArticleUpdateDto
{
    public long Id { get; set; }
	public string Text { get; set; }
	public long ContentId { get; set; }
	public IFormFile Image { get; set; }
}
