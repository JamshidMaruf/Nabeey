using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.ContentAudios;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class ContentAudioService : IContentAudioService
{
	private readonly IMapper mapper;
	private readonly IRepository<ContentCategory> categoryRepository;
	private readonly IRepository<ContentAudio> contentAudioRepository;
	private readonly IAssetService assetService;
	public ContentAudioService(
		IMapper mapper,
		IAssetService assetService,
		IRepository<ContentCategory> categoryRepository,
		IRepository<ContentAudio> contentAudioRepository)
	{
		this.mapper = mapper;
		this.assetService = assetService;
		this.categoryRepository = categoryRepository;
		this.contentAudioRepository = contentAudioRepository;
	}

	public async ValueTask<ContentAudioResultDto> AddAsync(ContentAudioCreationDto dto)
	{
		var category = await this.categoryRepository.SelectAsync(c => c.Id.Equals(dto.CategoryId))
					  ?? throw new NotFoundException("This content is not found");

		if (dto.Audio is null)
			throw new NotFoundException("This audio is not found");

		AssetCreationDto asset = new AssetCreationDto { FormFile = dto.Audio };

		var audio = await this.assetService.UploadAsync(asset, UploadType.Audios);
		var mappedAudio = new ContentAudio
		{
			AudioId = audio.Id,
			CategoryId = category.Id,
			Title = dto.Title,
			Description = dto.Description,
			Audio = audio,
			Category = category
		};
		await this.contentAudioRepository.InsertAsync(mappedAudio);
		await this.contentAudioRepository.SaveAsync();

		return this.mapper.Map<ContentAudioResultDto>(mappedAudio);
	}

	public async ValueTask<bool> RemoveAsync(long id)
	{
		var existAudio = await this.contentAudioRepository.SelectAsync(a => a.Id.Equals(id))
						?? throw new NotFoundException("This content audio is not found");

		bool isChecked = await this.assetService.RemoveAsync(existAudio.Audio);
		this.contentAudioRepository.Delete(existAudio);
		await this.contentAudioRepository.SaveAsync();
		return true;
	}

	public async ValueTask<ContentAudioResultDto> RetrieveByIdAsync(long id)
	{
		var existAudio =
			await this.contentAudioRepository.SelectAsync(a => a.Id.Equals(id), includes: new[] { "Audio" })
			?? throw new NotFoundException("This content audio is not found");

		return this.mapper.Map<ContentAudioResultDto>(existAudio);
	}

	public async ValueTask<IEnumerable<ContentAudioResultDto>> RetrieveAsync(PaginationParams @params, Filter filter, string search)
	{
		var existAudios = await this.contentAudioRepository.SelectAll(includes: new[] { "Audio" })
			.ToPaginate(@params)
			.ToListAsync();

		return this.mapper.Map<IEnumerable<ContentAudioResultDto>>(existAudios);
	}

	public async ValueTask<IEnumerable<ContentAudioResultDto>> RetrieveAllByCategoryIdAsync(long categoryId)
	{
		var audios = await this.contentAudioRepository.SelectAll(expression: audio => audio.CategoryId == categoryId, includes: new[] { "Audio" }).ToListAsync();
		return this.mapper.Map<IEnumerable<ContentAudioResultDto>>(audios);
	}
}