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
	private readonly IRepository<ContentCategory> categoryRepository;
	public ArticleService(
		IMapper mapper,
		IAssetService assetService,
		IRepository<User> userRepository,
		IRepository<Article> articleRepository,
		IRepository<ContentCategory> categoryRepository)
	{
		this.mapper = mapper;
		this.assetService = assetService;
		this.userRepository = userRepository;
		this.articleRepository = articleRepository;
		this.categoryRepository = categoryRepository;
	}

	public async ValueTask<ArticleResultDto> AddAsync(ArticleCreationDto dto)
	{
		var user = await this.userRepository.SelectAsync(u => u.Id.Equals(dto.UserId))
			?? throw new NotFoundException("This user is not Found");

		var imageAsset = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image }, UploadType.Images);

		var existCategory = await this.categoryRepository.SelectAsync(a => a.Id.Equals(dto.CategoryId))
			?? throw new NotFoundException($"This content is not found with id : {dto.CategoryId}");

		var createImage = new Asset
		{
			FileName = imageAsset.FileName,
			FilePath = imageAsset.FilePath,
		};

		var mapped = new Article
		{
			Text = dto.Text,
			CategoryId = dto.CategoryId,
			Category = existCategory,
			UserId = dto.UserId,
			User = user,
			ImageId = imageAsset.Id,
			Image = createImage,
		};

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
		var existArticle = await this.articleRepository.SelectAsync(expression: u => u.Id == id, includes: new[] { "Image" })
		   ?? throw new NotFoundException($"This article is not found with id : {id}");

		return this.mapper.Map<ArticleResultDto>(existArticle);
	}

	public async ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllByUserIdAsync(long userId)
	{
		var existUser = await this.userRepository.SelectAsync(expression: u => u.Id == userId, includes: new[] { "Image" })
			?? throw new NotFoundException("This user is not found");

		var userArticles = await this.articleRepository.SelectAll(a => a.UserId == userId).ToListAsync();

		return this.mapper.Map<IEnumerable<ArticleResultDto>>(userArticles);
	}

	public async ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllByCategoryIdAsync(long categoryId)
	{
		var existCategory = await this.categoryRepository.SelectAsync(expression: u => u.Id == categoryId, includes: new[] { "Image" })
			?? throw new NotFoundException("This user is not found");

		var contentArticles = await this.articleRepository.SelectAll(a => a.UserId == categoryId).ToListAsync();

		return this.mapper.Map<IEnumerable<ArticleResultDto>>(contentArticles);
	}

	public async ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null)
	{
		var allArticles = (await this.articleRepository.SelectAll(includes: new[] { "Image" })
			.ToPaginate(@params)
			.ToListAsync());

		if (search is not null)
			allArticles = allArticles.Where(d => d.Text.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

		return this.mapper.Map<IEnumerable<ArticleResultDto>>(allArticles);
	}
}