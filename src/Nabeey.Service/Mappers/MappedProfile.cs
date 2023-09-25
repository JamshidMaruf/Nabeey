﻿using AutoMapper;
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
using Nabeey.Service.DTOs.QuestionAnswers;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Service.Mappers;

public class MappedProfile : Profile
{
    public MappedProfile()
    {
        // ContentCategory
        CreateMap<ContentCategory, ContentCategoryResultDto>();
        CreateMap<ContentCategoryCreationDto, ContentCategory>();
        CreateMap<ContentCategoryUpdateDto, ContentCategory>();

        // ContentImage
        CreateMap<ContentImage, ContentImageResultDto>();
        CreateMap<ContentImageCreationDto, ContentImage>();
        CreateMap<ContentImageUpdateDto, ContentImage>();

        // ContentVideo
        CreateMap<ContentVideo, ContentVideoResultDto>();
        CreateMap<ContentVideoCreationDto, ContentVideo>();
        CreateMap<ContentVideoUpdateDto, ContentVideo>();

        // ContentAudio

        CreateMap<ContentAudio, ContentCategoryResultDto>().ReverseMap();
        CreateMap<ContentAudioCreationDto, ContentCategory>().ReverseMap();


        //User
        CreateMap<User, UserResultDto>();
        CreateMap<UserCreationDto, User>();
        CreateMap<UserUpdateDto, User>();

        // Answer
        CreateMap<Answer, AnswerResultDto>();
        CreateMap<AnswerCreationDto, Answer>();
        CreateMap<AnswerUpdateDto, Answer>();

        //Asset
        CreateMap<Asset, AssetResultDto>();
        CreateMap<AssetCreationDto, Asset>();

        //Article
        CreateMap<Article, ArticleResultDto>();
        CreateMap<ArticleCreationDto, Article>();
        CreateMap<ArticleUpdateDto, Article>();

        //Book
        CreateMap<Book, BookResultDto>();
        CreateMap<BookCreationDto, Book>();
        CreateMap<BookUpdateDto, Book>();

        //ContentBook
        CreateMap<ContentBook, ContentBookResultDto>();
        CreateMap<ContentBookCreationDto, ContentBook>();
        CreateMap<ContentBookUpdateDto, ContentBook>();

        //Content
        CreateMap<Content, ContentResultDto>();
        CreateMap<ContentCreationDto, Content>();
        CreateMap<ContentUpdateDto, Content>();

        //Question
        CreateMap<Question, QuestionResultDto>();
        CreateMap<QuestionCreationDto, Question>();
        CreateMap<QuestionUpdateDto, Question>();

        //QuestionAnswer
        CreateMap<QuestionAnswer, QuestionAnswerResultDto>();
        CreateMap<QuestionAnswerCreationDto, QuestionAnswer>();
        CreateMap<QuestionAnswerUpdateDto, QuestionAnswer>();

        //Quizz
        CreateMap<Quiz, QuizResultDto>();
        CreateMap<QuizCreationDto, Quiz>();
        CreateMap<QuizUpdateDto, Quiz>();

    }
}