using AutoMapper;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.QuizQuestions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.Answers;
using Nabeey.Service.DTOs.Articles;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.DTOs.ContentAudios;
using Nabeey.Service.DTOs.ContentCategories;
using Nabeey.Service.DTOs.ContentVideos;
using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.DTOs.QuizQuestions;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Answer
        CreateMap<Answer, AnswerResultDto>();
        CreateMap<AnswerCreationDto, Answer>();
        CreateMap<AnswerResultDto, Answer>();

        //Article
        CreateMap<Article, ArticleResultDto>();
        CreateMap<ArticleCreationDto, Article>();
        CreateMap<ArticleUpdateDto, Article>();

        //Asset 
        CreateMap<Asset, AssetCreationDto>().ReverseMap();
        CreateMap<AssetCreationDto, Asset>().ReverseMap();

        //Book
        CreateMap<Book, BookResultDto>();
        CreateMap<BookCreationDto, Book>();
        CreateMap<BookUpdateDto, Book>();

        // ContentAudio
        CreateMap<ContentAudio, ContentCategoryResultDto>().ReverseMap();
        CreateMap<ContentAudioCreationDto, ContentCategory>().ReverseMap();

        // ContentCategory
        CreateMap<ContentCategory, ContentCategoryResultDto>();
        CreateMap<ContentCategoryCreationDto, ContentCategory>();
        CreateMap<ContentCategoryResultDto, ContentCategory>();


        // ContentVideo
        CreateMap<ContentVideo, ContentVideoResultDto>();
        CreateMap<ContentVideoCreationDto, ContentVideo>();
        CreateMap<ContentVideoUpdateDto, ContentVideo>();

        //Question
        CreateMap<Question, QuestionResultDto>();
        CreateMap<QuestionCreationDto, Question>();
        CreateMap<QuestionUpdateDto, Question>();

        //QuizQuestion
        CreateMap<QuizQuestion, QuizQuestionResultDto>();
        CreateMap<QuizQuestionUpdateDto, QuizQuestion>();
        CreateMap<QuizQuestionResultDto, QuizQuestion>();

        //Quiz
        CreateMap<Quiz, ResultDto>();
        CreateMap<QuizCreationDto, Quiz>();
        CreateMap<QuizUpdateDto, Quiz>();

        //User
        CreateMap<User, UserResultDto>();
        CreateMap<UserCreationDto, User>();
        CreateMap<UserUpdateDto, User>();
    }
}