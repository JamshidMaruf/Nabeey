using Nabeey.Service.DTOs.Question;
using Nabeey.Service.DTOs.Quizzes.QuizQuestions;

namespace Nabeey.Service.Interfaces;

public interface IQuizQuestionService
{
    Task<QuizQuestionResultDto> AddAsync(QuizQuestionCreationDto dto);
    Task<QuizQuestionResultDto> ModifyAsync(QuizQuestionUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<QuizQuestionResultDto> RetrieveAsync(long id);
    Task<IEnumerable<Question>> RetrieveByQuiz(long id);
    Task<IEnumerable<QuizQuestionResultDto>> RetrieveAllAsync();
}
