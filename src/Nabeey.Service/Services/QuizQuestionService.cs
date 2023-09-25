using AutoMapper;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Service.DTOs.Question;
using Nabeey.Domain.Entities.QuizQuestions;
using Nabeey.Service.DTOs.QuizQuestions;

namespace Nabeey.Service.Services;

public class QuizQuestionService : IQuizQuestionService
{
    private readonly IRepository<QuizQuestion> quizQuestionRepository;
    private readonly IRepository<Question> questionRepository;
    private readonly IRepository<Quiz> quizRepository;
    private readonly IMapper mapper;
    public QuizQuestionService(IMapper mapper, IRepository<Quiz> quizRepository,
                               IRepository<QuizQuestion> quizQuestionRepository,
                               IRepository<Question> questionRepository)
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

        this.mapper.Map(dto, quizQuestion);
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
        var quizQuestion = await this.quizQuestionRepository.SelectAsync(q => q.Id == id)
            ?? throw new NotFoundException($"This quiz, question is not found with id : {id}");

        return this.mapper.Map<QuizQuestionResultDto>(quizQuestion);
    }

    public async ValueTask<IEnumerable<QuizQuestionResultDto>> RetrieveAllAsync()
    {
        var allQuizQuestion = await this.quizQuestionRepository.SelectAll().ToListAsync();
        return this.mapper.Map<IEnumerable<QuizQuestionResultDto>>(allQuizQuestion);
    }

    public async ValueTask<IEnumerable<QuestionResultDto>> RetrieveByQuiz(long id)
    {
        var existQuiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(id))
            ?? throw new NotFoundException("This quiz is not found");

        IEnumerable<Question> questions = new List<Question>();
        var quizQuestions = await this.quizQuestionRepository.SelectAll(includes: new[] { "Quiz","Question"}).ToListAsync();

        foreach (var item in quizQuestions)
        {
            if (item.QuizId == existQuiz.Id)
            {
               questions = questions.Append(item.Question);
            }
        }

        ShuffleQuestions(questions);

        return this.mapper.Map<IEnumerable<QuestionResultDto>>(questions);
    }

    private IEnumerable<Question> ShuffleQuestions(IEnumerable<Question> questions)
    {
        List<Question> questionList = questions.ToList();
        Random random = new Random();
        int n = questionList.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Question value = questionList[k];
            questionList[k] = questionList[n];
            questionList[n] = value;
        }
        return questionList;
    }
}