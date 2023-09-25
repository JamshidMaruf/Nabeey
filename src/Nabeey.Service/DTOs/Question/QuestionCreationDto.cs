using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.Question;

public class QuestionCreationDto
{
	public string Text { get; set; }
	public IFormFile File { get; set; }
}
