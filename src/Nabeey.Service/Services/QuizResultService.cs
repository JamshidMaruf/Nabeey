using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.QuestionAnswers;
using Nabeey.Domain.Entities.QuizQuestions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.Certificates;
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
	private IRepository<QuizQuestion> quizQuestionRepository;
	private IRepository<QuestionAnswer> questionAnswerRepository;
	public QuizResultService(
		IMapper mapper,
		IRepository<User> userRepository,
		IRepository<Quiz> quizRepository,
		IRepository<QuizResult> quizResultRepository,
		IRepository<QuizQuestion> quizQuestionRepository,
		IRepository<QuestionAnswer> questionAnswerRepository)
	{
		this.mapper = mapper;
		this.userRepository = userRepository;
		this.quizRepository = quizRepository;
		this.quizResultRepository = quizResultRepository;
		this.quizQuestionRepository = quizQuestionRepository;
		this.questionAnswerRepository = questionAnswerRepository;
	}

	public async ValueTask<ResultDto> RetrieveByUserIdAsync(long userId, long quizId)
	{
		var quiz = await this.quizRepository.SelectAsync(expression: q => q.Id.Equals(quizId), includes: new[] { "User", "ContentCategory" })
				   ?? throw new NotFoundException("This quiz is not found");

		var user = await this.userRepository.SelectAsync(u => u.Id.Equals(userId))
					?? throw new NotFoundException("This user is not found");

		var questionAnswers = await this.questionAnswerRepository.SelectAll(t => t.UserId.Equals(userId)
							  && t.QuizId.Equals(quizId), includes: new[] { "Quiz" })
							  .ToListAsync();

		var quizQuestionCount = await quizQuestionRepository.SelectAll(qq => qq.QuizId == quizId).CountAsync();
		var correctAnswers = questionAnswers.Where(t => t.IsTrue).Count();
		var incorrectAnswers = quizQuestionCount - correctAnswers;
		var ball = Math.Round((double)(correctAnswers * 100) / quizQuestionCount);

		ResultDto resultDto = new()
		{
			CorrectAnswers = correctAnswers,
			IncorrectAnswers = incorrectAnswers,
			Ball = ball,
			QuestionCount = quizQuestionCount,
			Quiz = this.mapper.Map<QuizResultDto>(quiz)
		};

		var existQuizResult = await this.quizResultRepository.SelectAsync(quizResult => quizResult.QuizId.Equals(quizId));
		if (existQuizResult is null)
		{
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
		}

		return resultDto;
	}

	public async ValueTask<IEnumerable<ResultDto>> RetrieveAllQuizIdAsync(long quizId)
	{
        var quizResults = await this.quizResultRepository.SelectAll(t => t.QuizId.Equals(quizId), includes: new[] { "Quiz.User", "Quiz.ContentCategory" })
                              .ToListAsync();

        var result = new List<ResultDto>();
		foreach (var quizResult in quizResults)
		{
			var quizQuestionCount = await quizQuestionRepository.SelectAll(qq => qq.QuizId == quizId).CountAsync();

			result.Add(new ResultDto()
			{
				CorrectAnswers = quizResult.CorrectAnswerCount,
				IncorrectAnswers = quizResult.IncorrectAnswerCount,
				Ball = quizResult.Ball,
				QuestionCount = quizQuestionCount,
				Quiz = this.mapper.Map<QuizResultDto>(quizResult.Quiz),
			});
		}

		return result;
	}

	public async ValueTask<IEnumerable<UserRatingDto>> RetrieveAllUserResultsAsync()
	{
		var results = this.quizResultRepository.SelectAll().AsEnumerable().GroupBy(result => result.UserId);

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
		for (var i = 1; i <= result.Count; i++)
			result[i - 1].Rating = i;

		return result;
	}

	public async ValueTask<UserRatingDto> RetrieveUserResultByIdAsync(long id)
	{
		var results = this.quizResultRepository.SelectAll().AsEnumerable().GroupBy(result => result.UserId);

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
		for (var i = 1; i <= result.Count; i++)
			result[i - 1].Rating = i;

		return result.FirstOrDefault(result => result.User.Id.Equals(id));
	}

    public ValueTask<CertificateResultDtoDto> RetrieveUserCertificateAsync(long userId, long quizId)
    {
        throw new NotImplementedException();
    }
}