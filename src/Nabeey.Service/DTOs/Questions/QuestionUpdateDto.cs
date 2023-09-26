using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.Questions;

public class QuestionUpdateDto
{
    public long Id { get; set; }
	public string Text { get; set; }
	public IFormFile Image { get; set; }
}
