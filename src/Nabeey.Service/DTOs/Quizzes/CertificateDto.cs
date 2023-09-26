using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Service.DTOs.Quizzes;

public class CertificateDto
{
	public UserResultDto User { get; set; }
	public AssetResultDto File { get; set; }
	public QuizResultDto Quiz { get; set; }
}
