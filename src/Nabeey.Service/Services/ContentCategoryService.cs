using AutoMapper;
using Nabeey.Domain.Enums;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Assets;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Service.DTOs.ContentCategories;

namespace Nabeey.Service.Services;

public class ContentCategoryService : IContentCategoryService
{
	private readonly IMapper mapper;
	private readonly IRepository<ContentCategory> repository;
	private readonly IAssetService assetService;

	public ContentCategoryService(IRepository<ContentCategory> repository, IMapper mapper, IAssetService assetService)
	{
		this.mapper = mapper;
		this.repository = repository;
		this.assetService = assetService;
	}

	public async ValueTask<ContentCategoryResultDto> AddAsync(ContentCategoryCreationDto dto)
	{
		var category = await this.repository.SelectAsync(expression: c => c.Name.ToLower().Equals(dto.Name.ToLower()),
			includes: new[] { "Image", "Books", "Audios", "Videos", "Articles" });
		if (category is not null)
			throw new AlreadyExistException("This category is already exists");

		if (dto.Image is null)
			throw new NotFoundException("Image is not found");

		var uploadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image }, UploadType.Images);
		var image = new Asset
		{
			FileName = uploadedImage.FileName,
			FilePath = uploadedImage?.FilePath,
		};

		var mappedCategory = new ContentCategory
		{
			Description = dto.Description,
			Name = dto.Name,
			ImageId = image.Id,
			Image = image
		};
		await this.repository.InsertAsync(mappedCategory);
		await this.repository.SaveAsync();

		return this.mapper.Map<ContentCategoryResultDto>(mappedCategory);
	}

    public async ValueTask<ContentCategoryResultDto> ModifyAsync(ContentCategoryUpdateDto dto)
    {
        var category = await this.repository.SelectAsync(c => c.Id.Equals(dto.Id), includes: new[] {"Image"})
            ?? throw new NotFoundException("This category is not found");

        if (!category.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase))
        {
            var existCategory = await this.repository.SelectAsync(c => c.Name.ToLower().Equals(dto.Name.ToLower()));
            if (existCategory is not null)
                throw new AlreadyExistException("This category is already exists");
        }

        var uploadedImage = new Asset();

        if (dto.Image is not null)
        {
            uploadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image }, UploadType.Images);
        }

        category.Description = dto.Description;
        category.Name = dto.Name;

        if (uploadedImage.Id > 0)
        {
            category.ImageId = uploadedImage.Id;
            category.Image.FileName = uploadedImage.FileName;
            category.Image.FilePath = uploadedImage.FilePath;
        }

        this.repository.Update(category);
        await this.repository.SaveAsync();

        return this.mapper.Map<ContentCategoryResultDto>(category);
    }

    public async ValueTask<bool> RemoveAsync(long id)
	{
		var category = await this.repository.SelectAsync(c => c.Id.Equals(id))
			?? throw new NotFoundException("This category is not found");

		this.repository.Delete(category);
		await this.repository.SaveAsync();
		return true;
	}

	public async ValueTask<ContentCategoryResultDto> RetrieveByIdAsync(long id)
	{
		var category = await this.repository.SelectAsync(expression: c => c.Id.Equals(id), includes: new[] { "Image", "Books.Image", "Books.File", "Audios.Audio", "Videos", "Articles.Image" })
			?? throw new NotFoundException("This category is not found");

		return this.mapper.Map<ContentCategoryResultDto>(category);
	}

	public async ValueTask<IEnumerable<ContentCategoryResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null)
	{
		var categories = await this.repository.SelectAll(includes: new[] { "Image", "Books.Image", "Books.File", "Audios.Audio", "Videos", "Articles.Image" })
			.ToPaginate(@params)
			.ToListAsync();
		
		if (!string.IsNullOrEmpty(search))
			categories = categories.Where(category => category.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

		return this.mapper.Map<IEnumerable<ContentCategoryResultDto>>(categories);
	}
}
