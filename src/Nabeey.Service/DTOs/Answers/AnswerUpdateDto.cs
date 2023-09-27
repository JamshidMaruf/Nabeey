using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.Answers;

public class AnswerUpdateDto
{
    public long Id { get; set; }
	public string Text { get; set; }
	public IFormFile Asset { get; set; }
    public bool IsTrue { get; set; }
    public long QuestionId { get; set; }
}
