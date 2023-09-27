using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.Questions;

public class QuestionCreationDto
{
	public string Text { get; set; }
	public IFormFile? Image { get; set; }
}