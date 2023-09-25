using Microsoft.AspNetCore.Http;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Contexts;

namespace Nabeey.Service.DTOs.Books;

public class BookCreationDto
{
	public string Title { get; set; }
	public string Author { get; set; }
	public string Description { get; set; }
	public IFormFile File { get; set; }
	public IFormFile Image { get; set; }
}
