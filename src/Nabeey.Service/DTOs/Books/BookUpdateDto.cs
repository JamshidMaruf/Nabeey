using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.Books;

public class BookUpdateDto
{
    public long Id { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public string Description { get; set; }
	public IFormFile File { get; set; }
	public IFormFile Image { get; set; }
}
