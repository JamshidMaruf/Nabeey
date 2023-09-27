using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Quizzes;

namespace Nabeey.Service.Interfaces;

public interface IQuizService
{
	ValueTask<QuizResultDto> AddAsync(QuizCreationDto dto);
	ValueTask<QuizResultDto> ModifyAsync(QuizUpdateDto dto);
	ValueTask<bool> DeleteAsync(long id);
	ValueTask<QuizResultDto> RetrieveByIdAsync(long id);
	ValueTask<IEnumerable<QuizResultDto>> RetrieveByContentCategoryIdAsync(long contentCategoryId);
	ValueTask<IEnumerable<QuizResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null);
}
