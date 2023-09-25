using AutoMapper;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.QuizQuestions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.Articles;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.DTOs.ContentAudio;
using Nabeey.Service.DTOs.ContentCategories;
using Nabeey.Service.DTOs.ContentImages;
using Nabeey.Service.DTOs.Contents;
using Nabeey.Service.DTOs.ContentVideos;
using Nabeey.Service.DTOs.Question;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.DTOs.Quizzes.QuizQuestions;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Service.Mappers;

public class MappedProfile : Profile
{
    public MappedProfile()
    {
        // Content
        CreateMap<Content, ContentCreationDto>().ReverseMap();
        CreateMap<Content, ContentUpdateDto>().ReverseMap();
        CreateMap<Content, ContentResultDto>().ReverseMap();

        // ContentCategory
        CreateMap<ContentCategory, ContentCategoryCreationDto>().ReverseMap();
        CreateMap<ContentCategory, ContentCategoryUpdateDto>().ReverseMap();
        CreateMap<ContentCategory, ContentCategoryResultDto>().ReverseMap();

        //Quiz Question
        CreateMap<QuizQuestion, QuizQuestionCreationDto>().ReverseMap();
        CreateMap<QuizQuestion, QuizQuestionResultDto>().ReverseMap();
        CreateMap<QuizQuestionUpdateDto, QuizQuestion>().ReverseMap();

        // ContentImage
        CreateMap<ContentImage, ContentImageCreationDto>().ReverseMap();
        CreateMap<ContentImage, ContentImageUpdateDto>().ReverseMap();
        CreateMap<ContentImage, ContentImageResultDto>().ReverseMap();

        // ContentVideo
        CreateMap<ContentVideo, ContentVideoResultDto>().ReverseMap();
        CreateMap<ContentVideoCreationDto, ContentVideo>().ReverseMap();
        CreateMap<ContentVideoUpdateDto, ContentVideo>().ReverseMap();

        // ContentAudio
        CreateMap<ContentAudio, ContentCategoryResultDto>().ReverseMap();
        CreateMap<ContentAudioCreationDto, ContentCategory>().ReverseMap();

        // User
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<UserCreationDto, User>().ReverseMap();
        CreateMap<UserUpdateDto, User>().ReverseMap();

        //Quiz
        CreateMap<Quiz, QuizResultDto>().ReverseMap();
        CreateMap<Quiz, QuizCreationDto>().ReverseMap();
        CreateMap<QuizUpdateDto, Quiz>().ReverseMap();

        //Quiz
        CreateMap<Book, BookResultDto>().ReverseMap();
        CreateMap<Book, BookCreationDto>().ReverseMap();
        CreateMap<BookUpdateDto, Book>().ReverseMap();

        //Article
        CreateMap<Article, ArticleResultDto>().ReverseMap();
        CreateMap<Article, ArticleCreationDto>().ReverseMap();
        CreateMap<ArticleUpdateDto, Article>().ReverseMap();

        //Question
        CreateMap<Question, QuestionResultDto>().ReverseMap();
        CreateMap<Question, QuestionCreationDto>().ReverseMap();
        CreateMap<QuestionUpdateDto, Question>().ReverseMap();
    }
}