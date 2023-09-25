using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Quizzes;

namespace Nabeey.Service.DTOs.Users;

public class UserCertificateDto
{
	public UserResultDto User { get; set; }
	public AssetResultDto File { get; set; }
	public QuizResultDto Quiz { get; set; }
}