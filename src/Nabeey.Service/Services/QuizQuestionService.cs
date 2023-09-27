using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.QuizQuestions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.DTOs.QuizQuestions;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class QuizQuestionService : IQuizQuestionService
{
	private readonly IMapper mapper;
	private readonly IRepository<Quiz> quizRepository;
	private readonly IRepository<Question> questionRepository;
	private readonly IRepository<QuizQuestion> quizQuestionRepository;
	public QuizQuestionService(
		IMapper mapper,
		IRepository<Quiz> quizRepository,
		IRepository<Question> questionRepository,
		IRepository<QuizQuestion> quizQuestionRepository)
	{
		this.mapper = mapper;
		this.quizRepository = quizRepository;
		this.questionRepository = questionRepository;
		this.quizQuestionRepository = quizQuestionRepository;
	}
	public async ValueTask<QuizQuestionResultDto> AddAsync(QuizQuestionCreationDto dto)
	{
		var existQuiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(dto.QuizId))
			?? throw new NotFoundException($"This quiz is not found with id : {dto.QuizId}");

		var existQuestion = await this.questionRepository.SelectAsync(q => q.Id.Equals(dto.QuestionId))
			?? throw new NotFoundException($"This question is not found with id : {dto.QuestionId}");

		var mapped = this.mapper.Map<QuizQuestion>(dto);
		mapped.Quiz = existQuiz;
		mapped.Question = existQuestion;
		await this.quizQuestionRepository.InsertAsync(mapped);
		await this.quizQuestionRepository.SaveAsync();

		return this.mapper.Map<QuizQuestionResultDto>(mapped);
	}

	public async ValueTask<QuizQuestionResultDto> ModifyAsync(QuizQuestionUpdateDto dto)
	{
		var quizQuestion = await this.quizQuestionRepository.SelectAsync(q => q.Id == dto.Id)
			?? throw new NotFoundException($"This quiz, question is not found with id : {dto.Id}");

		var existQuiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(dto.QuizId))
			?? throw new NotFoundException($"This quiz is not found with id : {dto.QuizId}");

		var existQuestion = await this.questionRepository.SelectAsync(q => q.Id.Equals(dto.QuestionId))
			?? throw new NotFoundException($"This question is not found with id : {dto.QuestionId}");

		this.mapper.Map(dto, quizQuestion);
		quizQuestion.Quiz = existQuiz;
		quizQuestion.Question = existQuestion;

		this.quizQuestionRepository.Update(quizQuestion);
		await this.quizQuestionRepository.SaveAsync();

		return this.mapper.Map<QuizQuestionResultDto>(quizQuestion);
	}

	public async ValueTask<bool> RemoveAsync(long id)
	{
		var quizQuestion = await this.quizQuestionRepository.SelectAsync(q => q.Id == id)
			?? throw new NotFoundException($"This quiz, question is not found with id : {id}");

		this.quizQuestionRepository.Delete(quizQuestion);
		await quizQuestionRepository.SaveAsync();

		return true;
	}

	public async ValueTask<QuizQuestionResultDto> RetrieveAsync(long id)
	{
		var quizQuestion = await this.quizQuestionRepository.SelectAsync(q => q.Id == id,
			includes: new[] { "Quiz.ContentCategory", "Question.Answers" })
			?? throw new NotFoundException($"This quiz, question is not found with id : {id}");

		return this.mapper.Map<QuizQuestionResultDto>(quizQuestion);
	}



	public async ValueTask<IEnumerable<QuizQuestionResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null)
	{
		var allQuizQuestion = await this.quizQuestionRepository.SelectAll(
			includes: new[] { "Quiz.ContentCategory", "Question.Answers" })
			.ToPaginate(@params)
			.ToListAsync();
		if (search is not null)
		{
			allQuizQuestion = allQuizQuestion.Where(d => d.Quiz.Name.Contains(search,
				StringComparison.OrdinalIgnoreCase)
				|| d.Question.Text.Contains(search, StringComparison.OrdinalIgnoreCase)
				|| d.Quiz.QuestionCount.ToString().Equals(search,
				StringComparison.OrdinalIgnoreCase)).ToList();
		}
		return this.mapper.Map<IEnumerable<QuizQuestionResultDto>>(allQuizQuestion);
	}

	public async ValueTask<IEnumerable<QuestionResultDto>> RetrieveAllByQuizIdAsync(long quizId)
	{
		var existQuiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(quizId))
			?? throw new NotFoundException("This quiz is not found");

		IEnumerable<Question> questions = new List<Question>();
		var quizQuestions = await this.quizQuestionRepository.SelectAll(includes: new[] { "Quiz", "Question" }).ToListAsync();

		foreach (var item in quizQuestions)
			if (item.QuizId == existQuiz.Id)
			{
				questions = questions.Append(item.Question);
				if (questions.Count() == existQuiz.QuestionCount)
				{
					break;
				}
			}

		questions = ShuffleQuestions(questions);

		return this.mapper.Map<IEnumerable<QuestionResultDto>>(questions);
	}

	private static IEnumerable<Question> ShuffleQuestions(IEnumerable<Question> questions)
	{
		List<Question> questionList = questions.ToList();
		var random = new Random();
		int n = questionList.Count;
		while (n > 1)
		{
			n--;
			int k = random.Next(n + 1);
			(questionList[k], questionList[n]) = (questionList[n], questionList[k]);
		}
		return questionList;
	}
}