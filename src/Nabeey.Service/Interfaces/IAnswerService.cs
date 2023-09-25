using Nabeey.Service.DTOs.Answers;

namespace Nabeey.Service.Interfaces;

public interface IAnswerService
{
    ValueTask<AnswerResultDto> AddAsync(AnswerCreationDto dto);
    ValueTask<AnswerResultDto> ModifyAsync(AnswerUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<AnswerResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<AnswerResultDto>> RetrieveAllAsync();
}

