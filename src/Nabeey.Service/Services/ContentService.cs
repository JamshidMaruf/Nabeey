using AutoMapper;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.DTOs.Contents;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Articles;
using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Articles;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.Contents;
using Nabeey.Service.DTOs.ContentVideos;
using Nabeey.Service.DTOs.ContentAudios;
using Nabeey.Service.DTOs.ContentCategories;

namespace Nabeey.Service.Services;

public class ContentService : IContentService
{
    private readonly IMapper mapper;
    private readonly IRepository<Content> contentRepository;
    private readonly IRepository<Article> articleRepository;
    private readonly IRepository<ContentBook> contentBookRepository;
    private readonly IRepository<ContentAudio> contentAudioRepository;
    private readonly IRepository<ContentVideo> contentVideoRepository;
    public ContentService(
        IMapper mapper,
        IRepository<Article> articleRepository,
        IRepository<Content> contentRepository,
        IRepository<ContentBook> contentBookRepository,
        IRepository<ContentAudio> contentAudioRepository,
        IRepository<ContentVideo> contentVideoRepository)
    {
        this.mapper = mapper;
        this.articleRepository = articleRepository;
        this.contentRepository = contentRepository;
        this.contentBookRepository = contentBookRepository;
        this.contentAudioRepository = contentAudioRepository;
        this.contentVideoRepository = contentVideoRepository;
    }

    public async ValueTask<ContentResultDto> RetrieveByIdAsync(long id)
    {
        var content = await this.contentRepository.SelectAsync(c => c.Id.Equals(id), includes:new[] { "ContentCategory" })
                      ?? throw new NotFoundException("This content is not found");

        var contentBooks = this.contentBookRepository.SelectAll(b => b.ContentId.Equals(id));
        
        var books = new List<BookResultDto>();
        foreach(var contentBook in contentBooks)
            books.Add(this.mapper.Map<BookResultDto>(contentBook.Book));

        var contentAudios = this.contentAudioRepository.SelectAll(a => a.ContentId.Equals(id));
        var audios = this.mapper.Map<IEnumerable<ContentAudioResultDto>>(contentAudios).ToList();

        var contentVideos = this.contentVideoRepository.SelectAll(v => v.ContentId.Equals(id));
        var videos = this.mapper.Map<IEnumerable<ContentVideoResultDto>>(contentVideos).ToList();

        var allArticles = this.articleRepository.SelectAll(a => a.ContentId.Equals(id));
        var articles = this.mapper.Map<IEnumerable<ArticleResultDto>>(allArticles).ToList();

        var contentCategory = this.mapper.Map<ContentCategoryResultDto>(content.ContentCategory);

        ContentResultDto result = new ContentResultDto()
        {
            Id = id,
            Books = books,
            Audios = audios,
            Videos = videos,
            Articles = articles,
            ContentCategory = contentCategory,
        };
        return result;
    }

    public async ValueTask<IEnumerable<ContentResultDto>> RetrieveAllAsnyc(PaginationParams @params, Filter filter, string search = null)
    {
        var contents = (await this.contentRepository.SelectAll(includes: new[] { "ContentCategory" }).ToListAsync()).ToPaginate(@params);
        var result = new List<ContentResultDto>();
        foreach(var content in contents)
        {
            var contentBooks = this.contentBookRepository.SelectAll(b => b.ContentId.Equals(content.Id));

            var books = new List<BookResultDto>();
            foreach (var contentBook in contentBooks)
                books.Add(this.mapper.Map<BookResultDto>(contentBook.Book));

            var contentAudios = this.contentAudioRepository.SelectAll(a => a.ContentId.Equals(content.Id));
            var audios = this.mapper.Map<IEnumerable<ContentAudioResultDto>>(contentAudios).ToList();

            var contentVideos = this.contentVideoRepository.SelectAll(v => v.ContentId.Equals(content.Id));
            var videos = this.mapper.Map<IEnumerable<ContentVideoResultDto>>(contentVideos).ToList();

            var allArticles = this.articleRepository.SelectAll(a => a.ContentId.Equals(content.Id));
            var articles = this.mapper.Map<IEnumerable<ArticleResultDto>>(allArticles).ToList();

            var contentCategory = this.mapper.Map<ContentCategoryResultDto>(content.ContentCategory);
            result.Add(new ContentResultDto()
            {
                Id = content.Id,
                Books = books,
                Audios = audios,
                Videos = videos,
                Articles = articles,
                ContentCategory = contentCategory,
            });
        }

        return result;
    }
}