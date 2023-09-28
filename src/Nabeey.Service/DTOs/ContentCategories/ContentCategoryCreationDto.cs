using Microsoft.AspNetCore.Http;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Contexts;

namespace Nabeey.Service.DTOs.ContentCategories;

public class ContentCategoryCreationDto
{
	public string Name { get; set; }
	public string Description { get; set; }
	public IFormFile Image { get; set; }
}
