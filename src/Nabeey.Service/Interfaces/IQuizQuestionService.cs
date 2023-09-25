using Nabeey.Domain.Entities.Questions;
using Nabeey.Service.DTOs.Question;
using Nabeey.Service.DTOs.Quizzes.QuizQuestions;

namespace Nabeey.Service.Interfaces;

public interface IQuizQuestionService
{
    ValueTask<QuizQuestionResultDto> AddAsync(QuizQuestionCreationDto dto);
    ValueTask<QuizQuestionResultDto> ModifyAsync(QuizQuestionUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<QuizQuestionResultDto> RetrieveAsync(long id);
    ValueTask<IEnumerable<QuestionResultDto>> RetrieveByQuiz(long id);
    ValueTask<IEnumerable<QuizQuestionResultDto>> RetrieveAllAsync();
}