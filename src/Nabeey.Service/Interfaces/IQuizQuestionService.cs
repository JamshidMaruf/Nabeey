using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.DTOs.QuizQuestions;

namespace Nabeey.Service.Interfaces;

public interface IQuizQuestionService
{
    Task<QuizQuestionResultDto> AddAsync(QuizQuestionCreationDto dto);
    Task<QuizQuestionResultDto> ModifyAsync(QuizQuestionUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<QuizQuestionResultDto> RetrieveAsync(long id);
    Task<IEnumerable<QuestionResultDto>> RetrieveByQuiz(long id);
    Task<IEnumerable<QuizQuestionResultDto>> RetrieveAllAsync();
}
