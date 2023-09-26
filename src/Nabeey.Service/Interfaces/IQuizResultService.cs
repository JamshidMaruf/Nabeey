using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Service.DTOs.Quizzes;

namespace Nabeey.Service.Interfaces;

public interface IQuizResultService
{
	ValueTask<ResultDto> RetrieveByUserIdAsync(long userId, long quizId);
	ValueTask<IEnumerable<ResultDto>> RetrieveAllQuizIdAsync(long quizId);
	ValueTask<IEnumerable<UserRatingDto>> RetrieveAllUserResultsAsync();
	ValueTask<CertificateDto> RetrieveUserCertificateAsync(long userId, long quizId);
}
