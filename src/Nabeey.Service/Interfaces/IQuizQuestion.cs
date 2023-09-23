using Nabeey.Domain.Entities.Questions;
using Nabeey.Service.DTOs.Quizzes.QuizQuestions;

namespace Nabeey.Service.Interfaces;

public interface IQuizQuestion
{
    Task<QuizQuestionResultDto> AddAsync(QuizQuestionCreationDto dto);
    Task<QuizQuestionResultDto> ModifyAsync(QuizQuestionUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<QuizQuestionResultDto> RetrieveAsync(long id);
    Task<IEnumerable<Question>> RetrieveByQuiz(long id);
    Task<IEnumerable<QuizQuestionResultDto>> RetrieveAllAsync();
}
