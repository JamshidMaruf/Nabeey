using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Questions;

namespace Nabeey.Service.Interfaces;

public interface IQuestionService
{
    ValueTask<QuestionResultDto> AddAsync(QuestionCreationDto dto);
    ValueTask<QuestionResultDto> ModifyAsync(QuestionUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<QuestionResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<QuestionResultDto>> RetrieveAllAsync(PaginationParams);
}
