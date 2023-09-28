using Nabeey.Service.DTOs.QuestionAnswers;

namespace Nabeey.Service.Interfaces;

public interface IQuestionAnswerService
{
	ValueTask<QuestionAnswerResultDto> AddAsync(QuestionAnswerCreationDto dto);
	ValueTask<QuestionAnswerResultDto> ModifyAsync(QuestionAnswerUpdateDto dto);
}
