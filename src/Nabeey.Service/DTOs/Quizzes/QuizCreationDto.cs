using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Service.DTOs.Quizzes;

public class QuizCreationDto
{
	public string Name { get; set; }
	public string Description { get; set; }
	public int QuestionCount { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public long UserId { get; set; }
	public long ContentCategoryId { get; set; }
}