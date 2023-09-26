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
    private readonly IRepository<Quiz> quizRepository;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<ContentCategory> categoryRepository;
    private readonly IMapper mapper;
    public QuizService(IMapper mapper, IRepository<Quiz> quizRepository, IRepository<ContentCategory> categoryRepository, IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.quizRepository = quizRepository;
        this.categoryRepository = categoryRepository;
        this.userRepository = userRepository;
    }
    public async ValueTask<QuizResultDto> AddAsync(QuizCreationDto dto)
    {
        var existQuiz = await this.quizRepository.SelectAsync(q => q.Name.Equals(dto.Name));
        if (existQuiz is not null)
            throw new AlreadyExistException($"This quiz already exist with id : {dto.Name}");

        var existCategory = await this.categoryRepository.SelectAsync(c => c.Id.Equals(dto.ContentCategoryId))
           ?? throw new NotFoundException($"This Content category is not found with id : {dto.ContentCategoryId}");

        var existUser = await this.userRepository.SelectAsync(c => c.Id.Equals(dto.UserId))
            ?? throw new NotFoundException($"This Content category is not found with id : {dto.UserId}");

        var mappedQuiz = this.mapper.Map<Quiz>(dto);
        mappedQuiz.User = existUser;
        mappedQuiz.ContentCategory = existCategory;

        await this.quizRepository.InsertAsync(mappedQuiz);
        await this.quizRepository.SaveAsync();

        return this.mapper.Map<QuizResultDto>(mappedQuiz);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existQuiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(id))
            ?? throw new NotFoundException($"This quiz is not found with id : {id}");

        this.quizRepository.Delete(existQuiz);
        await this.quizRepository.SaveAsync();

        return true;
    }

    public async ValueTask<QuizResultDto> ModifyAsync(QuizUpdateDto dto)
    {
        var existQuiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This quiz is not found with id : {dto.Id}");

        this.mapper.Map(dto, existQuiz);
        this.quizRepository.Update(existQuiz);
        await this.quizRepository.SaveAsync();

        return this.mapper.Map<QuizResultDto>(existQuiz);
    }

    public async ValueTask<QuizResultDto> RetrieveAsync(long id)
    {
        var existQuiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(id))
            ?? throw new NotFoundException($"This quiz is not found with id : {id}");

        return this.mapper.Map<QuizResultDto>(existQuiz);
    }
    public async ValueTask<IEnumerable<QuizResultDto>> RetrieveAllAsync()
    {
        var allQuizzes = await this.quizRepository.SelectAll(
            includes: new[] { "ContentCategory" }).ToListAsync();

        return this.mapper.Map<IEnumerable<QuizResultDto>>(allQuizzes);
    }
}
