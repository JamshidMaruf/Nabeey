using AutoMapper;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.Articles;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.DTOs.ContentAudios;
using Nabeey.Service.DTOs.ContentCategories;
using Nabeey.Service.DTOs.Contents;
using Nabeey.Service.DTOs.ContentVideos;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ContentCategory
        CreateMap<ContentCategory, ContentCategoryResultDto>().ReverseMap();
        CreateMap<ContentCategoryCreationDto, ContentCategory>().ReverseMap();
        CreateMap<ContentCategoryUpdateDto, ContentCategory>().ReverseMap();

        //Article
        CreateMap<Article, ArticleResultDto>();
        CreateMap<ArticleCreationDto, Article>();
        CreateMap<ArticleUpdateDto, Article>();

        // ContentVideo
        CreateMap<ContentVideo, ContentVideoResultDto>();
        CreateMap<ContentVideoCreationDto, ContentVideo>();
        CreateMap<ContentVideoUpdateDto, ContentVideo>();

        // ContentAudio
        CreateMap<ContentAudio, ContentAudioResultDto>();
        CreateMap<ContentAudioCreationDto, ContentCategory>();


        //User
        CreateMap<User, UserResultDto>();
        CreateMap<UserCreationDto, User>();
        CreateMap<UserUpdateDto, User>();

        //Article
        CreateMap<Article, ArticleResultDto>();
        CreateMap<ArticleCreationDto, Article>();
        CreateMap<ArticleUpdateDto, Article>();

        //Book
        CreateMap<Book, BookResultDto>();
        CreateMap<BookCreationDto, Book>();
        CreateMap<BookUpdateDto, Book>();

        //Content
        CreateMap<Content, ContentResultDto>().ReverseMap();

        //Question
        //CreateMap<Question, QuestionResultDto>();
        //CreateMap<QuestionCreationDto, Question>();
        //CreateMap<QuestionUpdateDto, Question>();

        //Quizz
        CreateMap<Quiz, QuizResultDto>().ReverseMap();
        CreateMap<QuizCreationDto, Quiz>().ReverseMap();
        CreateMap<QuizUpdateDto, Quiz>().ReverseMap();

    }
}