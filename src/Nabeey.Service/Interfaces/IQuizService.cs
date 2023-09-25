using Nabeey.Service.DTOs.Quizzes;

namespace Nabeey.Service.Interfaces;

public interface IQuizService
{
    ValueTask<QuizResultDto> AddAsync(QuizCreationDto dto);
    ValueTask<QuizResultDto> ModifyAsync(QuizUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<QuizResultDto> RetrieveAsync(long id);
    ValueTask<IEnumerable<QuizResultDto>> RetrieveAllAsync();
}
