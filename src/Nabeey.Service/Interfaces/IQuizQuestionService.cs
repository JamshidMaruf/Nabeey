using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.DTOs.QuizQuestions;

namespace Nabeey.Service.Interfaces;

public interface IQuizQuestionService
{
	ValueTask<QuizQuestionResultDto> AddAsync(QuizQuestionCreationDto dto);
	ValueTask<QuizQuestionResultDto> ModifyAsync(QuizQuestionUpdateDto dto);
	ValueTask<bool> RemoveAsync(long id);
	ValueTask<QuizQuestionResultDto> RetrieveAsync(long id);
	ValueTask<IEnumerable<QuizQuestionResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null);
	ValueTask<IEnumerable<QuestionResultDto>> RetrieveAllByQuizIdAsync(long quizId);
}