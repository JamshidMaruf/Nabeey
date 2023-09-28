using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Entities.Users;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Articles;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Helpers;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class ArticleService : IArticleService
{
    private readonly IMapper mapper;
    private readonly IAssetService assetService;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Article> articleRepository;
    private readonly IRepository<Content> contentRepository;
    public ArticleService(
        IMapper mapper,
        IAssetService assetService,
        IRepository<User> userRepository,
        IRepository<Article> articleRepository,
        IRepository<Content> contentRepository)
    {
        this.mapper = mapper;
        this.assetService = assetService;
        this.userRepository = userRepository;
        this.articleRepository = articleRepository;
        this.contentRepository = contentRepository;
    }
          
    public async ValueTask<ArticleResultDto> AddAsync(ArticleCreationDto dto)
    {
        if (HttpContextHelper.GetUserId != 0)
            throw new CustomException(401, "This user is not authorized");

        var user = await this.userRepository.SelectAsync(u => u.Id.Equals(HttpContextHelper.GetUserId))
            ?? throw new NotFoundException("This user is not Found");

        var imageAsset = new Asset();
        if (dto.Image != null)
        {
            imageAsset = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image }, UploadType.Images);
        }

        var existContent = await this.contentRepository.SelectAsync(a => a.Id.Equals(dto.ContentId))
            ?? throw new NotFoundException($"This content is not found with id : {dto.ContentId}");

        var createImage = new Asset()
        {
            FileName = imageAsset.FileName,
            FilePath = imageAsset.FilePath,
        };

        var mappedArticle = new Article()
        {
            Text = dto.Text,
            ContentId = dto.ContentId,
            Image = createImage
        };

        mappedArticle.Content = existContent;
        mappedArticle.User = user;
        mappedArticle.UserId = user.Id;
        mappedArticle.ImageId = imageAsset.Id;

        await this.articleRepository.InsertAsync(mappedArticle);
        await this.articleRepository.SaveAsync();

        return this.mapper.Map<ArticleResultDto>(mappedArticle);
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

    public async ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllByUserIdAsync(long userId)
    {
        var existUser = await this.userRepository.SelectAsync(u => u.Id == userId)
            ?? throw new NotFoundException("This user is not found");

        var userArticles = await this.articleRepository.SelectAll(a => a.UserId == userId).ToListAsync();
        
        return this.mapper.Map<IEnumerable<ArticleResultDto>>(userArticles);    
    }

    public async ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllByContentIdAsync(long contentId)
    {
        var existContent = await this.contentRepository.SelectAsync(u => u.Id == contentId)
            ?? throw new NotFoundException("This user is not found");

        var contentArticles = await this.articleRepository.SelectAll(a => a.UserId == contentId).ToListAsync();

        return this.mapper.Map<IEnumerable<ArticleResultDto>>(contentArticles);
    }

    public async ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var allArticles = (await this.articleRepository.SelectAll(includes: new[] { "Content", "Image" })
            .ToPaginate(@params)
            .ToListAsync());

        if (search is not null)
        {
            allArticles = allArticles.Where(d => d.Text.Contains(search,
                StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<ArticleResultDto>>(allArticles);
    }
}