using Nabeey.Service.DTOs.Users;

namespace Nabeey.Domain.Entities.Quizzes;

public class UserRatingDto
{
	public int Rating { get; set; }
	public double Ball { get; set; }
	public UserResultDto User { get; set; }
}