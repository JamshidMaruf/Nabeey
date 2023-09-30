﻿using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Service.DTOs.Certificates;

namespace Nabeey.Service.Interfaces;

public interface IQuizResultService
{
	ValueTask<ResultDto> RetrieveByUserIdAsync(long userId, long quizId);
	ValueTask<IEnumerable<ResultDto>> RetrieveAllQuizIdAsync(long quizId);
	ValueTask<IEnumerable<UserRatingDto>> RetrieveAllUserResultsAsync();
}
