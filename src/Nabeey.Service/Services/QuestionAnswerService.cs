using AutoMapper;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.QuestionAnswers;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.QuestionAnswers;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Helpers;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class QuestionAnswerService : IQuestionAnswerService
{
	private readonly IRepository<QuestionAnswer> questionAnswerRepository;
	private readonly IRepository<Answer> answerRepository;
	private readonly IRepository<Question> questionRepository;
	private readonly IRepository<User> userRepository;
	private readonly IRepository<Quiz> quizRepository;
	private readonly IMapper mapper;
	public QuestionAnswerService(IMapper mapper,
		IRepository<Quiz> quizRepository,
		IRepository<User> userRepository,
		IRepository<Answer> answerRepository,
		IRepository<Question> questionRepository,
		IRepository<QuestionAnswer> questionAnswerRepository)
	{
		this.mapper = mapper;
		this.quizRepository = quizRepository;
		this.userRepository = userRepository;
		this.answerRepository = answerRepository;
		this.questionRepository = questionRepository;
		this.questionAnswerRepository = questionAnswerRepository;
	}
	public async ValueTask<QuestionAnswerResultDto> AddAsync(QuestionAnswerCreationDto dto)
	{
        var user = await this.userRepository.SelectAsync(u => u.Id.Equals(dto.UserId))
            ?? throw new NotFoundException("This user is not Found");

		var existAnswer = await this.answerRepository.SelectAsync(a => a.Id == dto.AnswerId)
			?? throw new NotFoundException("This answer is not found");

		var existQuiz = await this.quizRepository.SelectAsync(a => a.Id == dto.QuizId)
			?? throw new NotFoundException("This quiz is not found");

		var existQuestion = await this.questionRepository.SelectAsync(a => a.Id == dto.QuestionId)
			?? throw new NotFoundException("This question is not found");

		var answer = await this.answerRepository.SelectAsync(answer => answer.Id == dto.AnswerId);
		var mapped = this.mapper.Map<QuestionAnswer>(dto);
		mapped.Answer = existAnswer;
		mapped.Quiz = existQuiz;
		mapped.User = user;
		mapped.UserId = user.Id;
		mapped.Question = existQuestion;
		mapped.IsTrue = answer.IsTrue;

		await this.questionAnswerRepository.InsertAsync(mapped);
		await this.questionAnswerRepository.SaveAsync();

		return this.mapper.Map<QuestionAnswerResultDto>(mapped);
	}

	public async ValueTask<QuestionAnswerResultDto> ModifyAsync(QuestionAnswerUpdateDto dto)
	{
		var existQuizAnswer = await this.questionAnswerRepository.SelectAsync(a => a.Id == dto.Id)
		   ?? throw new NotFoundException("This quiz answer is not found");

		var user = await this.userRepository.SelectAsync(u => u.Id.Equals(dto.UserId))
			?? throw new NotFoundException("This user is not Found");

		var existAnswer = await this.answerRepository.SelectAsync(a => a.Id == dto.AnswerId)
			?? throw new NotFoundException("This answer is not found");

		var existQuiz = await this.quizRepository.SelectAsync(a => a.Id == dto.QuizId)
			?? throw new NotFoundException("This quiz is not found");

		var existQuestion = await this.questionRepository.SelectAsync(a => a.Id == dto.QuestionId)
			?? throw new NotFoundException("This question is not found");

		this.mapper.Map(dto, existQuizAnswer);
		existQuizAnswer.Question = existQuestion;
		existQuizAnswer.Quiz = existQuiz;
		existQuizAnswer.Answer = existAnswer;
		existQuizAnswer.User = user;

		this.questionAnswerRepository.Update(existQuizAnswer);
		await questionAnswerRepository.SaveAsync();

		return this.mapper.Map<QuestionAnswerResultDto>(existQuizAnswer);
	}
}
