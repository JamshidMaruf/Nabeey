using Nabeey.Service.DTOs.Quizzes;

namespace Nabeey.Service.Interfaces;

public interface IQuizService
{
    Task<QuizResultDto> AddAsync(QuizCreationDto dto);
    Task<QuizResultDto> ModifyAsync(QuizUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<QuizResultDto> RetrieveAsync(long id);
    Task<IEnumerable<QuizResultDto>> RetrieveAllAsync();
}
