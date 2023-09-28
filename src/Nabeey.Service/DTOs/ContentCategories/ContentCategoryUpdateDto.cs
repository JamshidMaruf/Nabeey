using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.ContentCategories;

public class ContentCategoryUpdateDto
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public IFormFile Image { get; set; }
}
