using Nabeey.Service.DTOs.Users;

namespace Nabeey.Domain.Entities.Quizzes;

public class UserRatingDto
{
	public short Rating { get; set; }
	public int Ball { get; set; }
	public UserResultDto User { get; set; }
}