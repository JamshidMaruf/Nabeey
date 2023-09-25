using Nabeey.Service.DTOs.QuizQuestions;

namespace Nabeey.Service.Interfaces;

public interface IQuizQuestionService
{
    ValueTask<QuizQuestionResultDto> AddAsync(QuizQuestionCreationDto dto);
    ValueTask<QuizQuestionResultDto> ModifyAsync(QuizQuestionUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<QuizQuestionResultDto> RetrieveAsync(long id);
    ValueTask<IEnumerable<QuizQuestionResultDto>> RetrieveByQuiz(long id);
    ValueTask<IEnumerable<QuizQuestionResultDto>> RetrieveAllAsync();
}
