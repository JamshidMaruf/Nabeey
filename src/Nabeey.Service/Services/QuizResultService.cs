using AutoMapper;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Interfaces;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.QuestionAnswers;

namespace Nabeey.Service.Services;

public class QuizResultService : IQuizResultService
{
    private IMapper mapper;
    private IRepository<User> userRepository;
    private IRepository<Quiz> quizRepository;
    private IRepository<QuestionAnswer> questionAnswerRepository;
    public QuizResultService(
        IMapper mapper,
        IRepository<User> userRepository,
        IRepository<Quiz> quizRepository,
        IRepository<QuestionAnswer> questionAnswerRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.quizRepository = quizRepository;
        this.questionAnswerRepository = questionAnswerRepository;
    }

    public async ValueTask<ResultDto> RetrieveByUserIdAsync(long userId, long quizId)
    {
        var quiz = await this.quizRepository.SelectAsync(q => q.Id.Equals(quizId))
                   ?? throw new NotFoundException("This quiz is not found");

        var user = await this.userRepository.SelectAsync(u => u.Id.Equals(userId))
                    ?? throw new NotFoundException("This user is not found");

        var questionAnswers = await this.questionAnswerRepository.SelectAll(t => t.UserId.Equals(userId)
                              && t.QuizId.Equals(quizId))
                              .ToListAsync();

        var correctAnswers = questionAnswers.Where(t => t.IsTrue).Count();
        var incorrectAnswers = questionAnswers.Where(t => !t.IsTrue).Count();
        var percentage = (correctAnswers * 100)/quiz.QuestionCount;

        ResultDto resultDto = new ResultDto()
        {
            CorrectAnswers = correctAnswers,
            IncorrectAnswers = incorrectAnswers,
            Percentage = percentage,
            Quiz = this.mapper.Map<QuizResultDto>(quiz)
        };
         
        return resultDto;
    }

    public async ValueTask<IEnumerable<ResultDto>> RetrieveAllQuizIdAsync(long quizId)
    {
        var questionAnswers = await this.questionAnswerRepository.SelectAll().ToListAsync();

        var result = new List<ResultDto>();
        foreach (var  questionAnswer in questionAnswers)
        {
            var allQuestion = this.questionAnswerRepository.SelectAll(t => t.QuizId.Equals(quizId) 
                              && t.UserId.Equals(questionAnswer.UserId));

            var correctAnswers = allQuestion.Where(t => t.IsTrue).Count();
            var incorrectAnswers = allQuestion.Where(t => !t.IsTrue).Count();
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

    public ValueTask<CertificateDto> RetrieveUserCertificateAsync(long userId, long quizId)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<UserRatingDto>> RetrieveAllUserResultsAsync()
    {
        throw new NotImplementedException();
    }
}