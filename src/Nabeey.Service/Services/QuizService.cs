using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class QuizService : IQuizService
{
    private readonly IMapper mapper;
    private readonly IRepository<Quiz> quizRepository;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<ContentCategory> categoryRepository;
    public QuizService(
        IMapper mapper,
        IRepository<Quiz> quizRepository,
        IRepository<User> userRepository,
        IRepository<ContentCategory> categoryRepository)
    {
        this.mapper = mapper;
        this.quizRepository = quizRepository;
        this.userRepository = userRepository;
        this.categoryRepository = categoryRepository;
    }
    public async ValueTask<QuizResultDto> AddAsync(QuizCreationDto dto)
    {
        var existQuiz = await this.quizRepository.SelectAsync(q => q.Name.ToLower().Equals(dto.Name.ToLower()));
        if (existQuiz is not null)
            throw new AlreadyExistException($"This quiz already exist with id : {dto.Name}");

        var existCategory = await this.categoryRepository.SelectAsync(c => c.Id.Equals(dto.ContentCategoryId))
            ?? throw new NotFoundException($"This content category is not found with id : {dto.ContentCategoryId}");

        var existUser = await this.userRepository.SelectAsync(c => c.Id.Equals(dto.UserId))
            ?? throw new NotFoundException($"This user is not found with id : {dto.UserId}");

        var mappedQuiz = this.mapper.Map<Quiz>(dto);
        mappedQuiz.StartTime = DateTime.Parse(mappedQuiz.StartTime.ToString());
        mappedQuiz.EndTime = DateTime.Parse(mappedQuiz.EndTime.ToString());

        await this.quizRepository.InsertAsync(mappedQuiz);
        await this.quizRepository.SaveAsync();

        mappedQuiz.ContentCategory = existCategory;
        mappedQuiz.User = existUser;

        return this.mapper.Map<QuizResultDto>(mappedQuiz);
    }

    public async ValueTask<QuizResultDto> ModifyAsync(QuizUpdateDto dto)
    {
        var quiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This quiz is not found with id : {dto.Id}");

        var existCategory = await this.categoryRepository.SelectAsync(c => c.Id.Equals(dto.ContentCategoryId))
            ?? throw new NotFoundException($"This content category is not found with id : {dto.ContentCategoryId}");

        var existUser = await this.userRepository.SelectAsync(c => c.Id.Equals(dto.UserId))
            ?? throw new NotFoundException($"This user is not found with id : {dto.UserId}");

        if (!quiz.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase))
        {
            var existQuiz = await this.quizRepository.SelectAsync(q => q.Name.ToLower().Equals(dto.Name.ToLower()));
            if (existQuiz is not null)
                throw new AlreadyExistException($"This quiz already exist with id : {dto.Name}");
        }

        this.mapper.Map(dto, quiz);
        quiz.StartTime = DateTime.Parse(quiz.StartTime.ToString());
        quiz.EndTime = DateTime.Parse(quiz.EndTime.ToString());

        this.quizRepository.Update(quiz);
        await this.quizRepository.SaveAsync();

        quiz.User = existUser;
        quiz.ContentCategory = existCategory;

        return this.mapper.Map<QuizResultDto>(quiz);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existQuiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(id))
            ?? throw new NotFoundException($"This quiz is not found with id : {id}");

        this.quizRepository.Delete(existQuiz);
        await this.quizRepository.SaveAsync();

        return true;
    }

    public async ValueTask<QuizResultDto> RetrieveAsync(long id)
    {
        var existQuiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(id),
            includes: new[] { "ContentCategory", "User" })
            ?? throw new NotFoundException($"This quiz is not found with id : {id}");

        return this.mapper.Map<QuizResultDto>(existQuiz);
    }
    public async ValueTask<IEnumerable<QuizResultDto>> RetrieveAllAsync()
    {
        var allQuizzes = await this.quizRepository.SelectAll(
            includes: new[] { "ContentCategory", "User" }).ToListAsync();

        return this.mapper.Map<IEnumerable<QuizResultDto>>(allQuizzes);
    }
}
