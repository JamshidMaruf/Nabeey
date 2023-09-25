using Microsoft.AspNetCore.Http;
using Nabeey.Domain.Entities.Assets;

namespace Nabeey.Service.DTOs.Answer;

public class AnswerCreationDto
{
	public string Text { get; set; }
	public long QuestionId { get; set; }
	public IFormFile File { get; set; }
}
