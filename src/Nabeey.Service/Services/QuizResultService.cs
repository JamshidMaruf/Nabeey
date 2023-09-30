using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.QuestionAnswers;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.DTOs.Users;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class QuizResultService : IQuizResultService
{
	private IMapper mapper;
	private IRepository<User> userRepository;
	private IRepository<Quiz> quizRepository;
	private IRepository<QuizResult> quizResultRepository;
	private IRepository<QuestionAnswer> questionAnswerRepository;
	public QuizResultService(
		IMapper mapper,
		IRepository<User> userRepository,
		IRepository<Quiz> quizRepository,
		IRepository<QuestionAnswer> questionAnswerRepository,
		IRepository<QuizResult> quizResultRepository)
	{
		this.mapper = mapper;
		this.userRepository = userRepository;
		this.quizRepository = quizRepository;
		this.questionAnswerRepository = questionAnswerRepository;
		this.quizResultRepository = quizResultRepository;
	}

	public async ValueTask<ResultDto> RetrieveByUserIdAsync(long userId, long quizId)
	{
		var quiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(quizId))
				   ?? throw new NotFoundException("This quiz is not found");

		var user = await this.userRepository.SelectAsync(u => u.Id.Equals(userId))
					?? throw new NotFoundException("This user is not found");

		var questionAnswers = await this.questionAnswerRepository.SelectAll(t => t.UserId.Equals(userId)
							  && t.QuizId.Equals(quizId), includes: new[] { "Quiz" })
							  .ToListAsync();

		var correctAnswers = questionAnswers.Where(t => t.IsTrue).Count();
		var incorrectAnswers = quiz.QuestionCount - correctAnswers;
		var ball = Math.Round((double)(correctAnswers * 100) / quiz.QuestionCount);

		ResultDto resultDto = new()
		{
			CorrectAnswers = correctAnswers,
			IncorrectAnswers = incorrectAnswers,
			Ball = ball,
			Quiz = this.mapper.Map<QuizResultDto>(quiz)
		};

		var quizResult = new QuizResult
		{
			UserId = userId,
			QuizId = quizId,
			Ball = correctAnswers,
			CorrectAnswerCount = correctAnswers,
			IncorrectAnswerCount = incorrectAnswers,
		};
		await this.quizResultRepository.InsertAsync(quizResult);
		await this.quizResultRepository.SaveAsync();

		return resultDto;
	}

	public async ValueTask<IEnumerable<ResultDto>> RetrieveAllQuizIdAsync(long quizId)
	{
		var questionAnswers = await this.questionAnswerRepository.SelectAll().ToListAsync();

		var result = new List<ResultDto>();
		foreach (var questionAnswer in questionAnswers)
		{
			var allQuestion = this.questionAnswerRepository.SelectAll(t => t.QuizId.Equals(quizId)
							  && t.UserId.Equals(questionAnswer.UserId), includes: new[] { "Quiz" });

			var correctAnswers = allQuestion.Where(t => t.IsTrue).Count();
			var incorrectAnswers = questionAnswer.Quiz.QuestionCount - correctAnswers;
			var percentage = (correctAnswers * 100) / questionAnswer.Quiz.QuestionCount;

			result.Add(new ResultDto()
			{
				CorrectAnswers = correctAnswers,
				IncorrectAnswers = incorrectAnswers,
				Percentage = percentage,
				Quiz = this.mapper.Map<QuizResultDto>(questionAnswer.Quiz)
			});
		}

		return result;
	}

	public async ValueTask<IEnumerable<UserRatingDto>> RetrieveAllUserResultsAsync()
	{
		var results = this.quizResultRepository.SelectAll().GroupBy(result => result.UserId);

		var result = new List<UserRatingDto>();
		foreach (var item in results)
		{
			long userId = item.Key;
			var user = await this.userRepository.SelectAsync(user => user.Id.Equals(userId));

			var itemResult = new UserRatingDto
			{
				Ball = item.Sum(t => t.Ball),
				User = this.mapper.Map<UserResultDto>(user)
			};
			result.Add(itemResult);
		}

		result = result.OrderByDescending(r => r.Ball).ToList();
		foreach (var item in result)
			item.Rating = item.Rating + 1;

		return result;
	}
}