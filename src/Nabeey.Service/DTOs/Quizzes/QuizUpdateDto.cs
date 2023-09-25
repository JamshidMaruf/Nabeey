namespace Nabeey.Service.DTOs.Quizzes;

public class QuizUpdateDto
{
    public long Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public int QuestionCount { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public long UserId { get; set; }
	public long ContentCategoryId { get; set; }
}