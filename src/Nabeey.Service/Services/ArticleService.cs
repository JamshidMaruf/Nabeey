using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Articles;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class ArticleService : IArticleService
{
    private readonly IMapper mapper;
    private readonly IAssetService assetService;
    private readonly IRepository<Article> articleRepository;
    private readonly IRepository<Content> contentRepository;
    public ArticleService(
        IMapper mapper, 
        IAssetService assetService,
        IRepository<Article> articleRepository, 
        IRepository<Content> contentRepository) 
    {
        this.mapper = mapper;
        this.assetService = assetService;
        this.articleRepository = articleRepository;
        this.contentRepository = contentRepository;
    }

    public async ValueTask<ArticleResultDto> AddAsync(ArticleCreationDto dto)
    {
        var imageAsset = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image }, UploadType.Images);

        var existContent = await this.contentRepository.SelectAsync(a => a.Id.Equals(dto.ContentId))
            ?? throw new NotFoundException($"This content is not found with id : {dto.ContentId}");

        var mapped = this.mapper.Map<Article>(dto);
        mapped.Content = existContent;
        mapped.Image = imageAsset;
        mapped.ImageId = imageAsset.Id;

        await this.articleRepository.InsertAsync(mapped);
        await this.articleRepository.SaveAsync();

        return this.mapper.Map<ArticleResultDto>(mapped);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existArticle = await this.articleRepository.SelectAsync(a => a.Id.Equals(id))
           ?? throw new NotFoundException($"This article is not found with id : {id}");

        await this.assetService.RemoveAsync(existArticle.Image);
        this.articleRepository.Delete(existArticle);
        await this.articleRepository.SaveAsync();

        return true;
    }

    public async ValueTask<ArticleResultDto> ModifyAsync(ArticleUpdateDto dto)
    {
        var existArticle = await this.articleRepository.SelectAsync(a => a.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This article is not found with id : {dto.Id}");

        this.mapper.Map(dto, existArticle);
        this.articleRepository.Update(existArticle);
        await this.articleRepository.SaveAsync();

        return this.mapper.Map<ArticleResultDto>(existArticle);
    }


    public async ValueTask<ArticleResultDto> RetrieveAsync(long id)
    {
        var existArticle = await this.articleRepository.SelectAsync(a => a.Id.Equals(id))
           ?? throw new NotFoundException($"This article is not found with id : {id}");

        return this.mapper.Map<ArticleResultDto>(existArticle);
    }

    public ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllByUserIdAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllByContentIdAsync(long contentId)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var allArticles = (await this.articleRepository.SelectAll(includes: new[] { "Content" })
            .ToPaginate(@params)
            .ToListAsync());

        return this.mapper.Map<IEnumerable<ArticleResultDto>>(allArticles);
    }
}