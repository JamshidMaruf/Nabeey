using AutoMapper;
using Nabeey.Domain.Entities.Answers;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Certificates;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.QuestionAnswers;
using Nabeey.Domain.Entities.Questions;
using Nabeey.Domain.Entities.QuizQuestions;
using Nabeey.Domain.Entities.Quizzes;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.Answers;
using Nabeey.Service.DTOs.Articles;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.DTOs.Certificates;
using Nabeey.Service.DTOs.ContentAudios;
using Nabeey.Service.DTOs.ContentCategories;
using Nabeey.Service.DTOs.ContentVideos;
using Nabeey.Service.DTOs.QuestionAnswers;
using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.DTOs.QuizQuestions;
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
		CreateMap<Article, ArticleResultDto>().ReverseMap(); ;
		CreateMap<ArticleCreationDto, Article>().ReverseMap(); ;
		CreateMap<ArticleUpdateDto, Article>().ReverseMap(); ;

		// ContentVideo
		CreateMap<ContentVideo, ContentVideoResultDto>().ReverseMap(); ;
		CreateMap<ContentVideoCreationDto, ContentVideo>().ReverseMap(); ;
		CreateMap<ContentVideoUpdateDto, ContentVideo>().ReverseMap(); ;

		// ContentAudio
		CreateMap<ContentAudio, ContentAudioResultDto>().ReverseMap(); ;
		CreateMap<ContentAudioCreationDto, ContentCategory>().ReverseMap(); ;


		//User
		CreateMap<User, UserResultDto>().ReverseMap();
		CreateMap<UserCreationDto, User>().ReverseMap();
		CreateMap<UserUpdateDto, User>().ReverseMap();

		//Answer
		CreateMap<Answer, AnswerResultDto>().ReverseMap();
		CreateMap<AnswerCreationDto, Answer>().ReverseMap();
		CreateMap<AnswerUpdateDto, Answer>().ReverseMap();

		//Article
		CreateMap<Article, ArticleResultDto>().ReverseMap(); ;
		CreateMap<ArticleCreationDto, Article>().ReverseMap(); ;
		CreateMap<ArticleUpdateDto, Article>().ReverseMap(); ;

		//Book
		CreateMap<Book, BookResultDto>().ReverseMap();
		CreateMap<BookCreationDto, Book>().ReverseMap();
		CreateMap<BookUpdateDto, Book>().ReverseMap();

		//Question
		CreateMap<Question, QuestionResultDto>().ReverseMap();
		CreateMap<Question, QuestionCreationDto>().ReverseMap();
		CreateMap<Question, QuestionUpdateDto>().ReverseMap();

		//Asset
		CreateMap<Asset, AssetResultDto>().ReverseMap();

		//Certificate
		CreateMap<Certificate, CertificateResultDto>();

		//Quiz
		CreateMap<Quiz, QuizResultDto>().ReverseMap();
		CreateMap<QuizCreationDto, Quiz>().ReverseMap();
		CreateMap<QuizUpdateDto, Quiz>().ReverseMap();

		//QuizQuestion
		CreateMap<QuizQuestion, QuizQuestionResultDto>().ReverseMap();
		CreateMap<QuizQuestion, QuizQuestionCreationDto>().ReverseMap();
		CreateMap<QuizQuestion, QuizQuestionUpdateDto>().ReverseMap();

        //QuestionAnswer
        CreateMap<QuestionAnswer, QuestionAnswerResultDto>().ReverseMap();
        CreateMap<QuestionAnswer, QuestionAnswerCreationDto>().ReverseMap();
        CreateMap<QuestionAnswer, QuestionAnswerUpdateDto>().ReverseMap();
    }
}