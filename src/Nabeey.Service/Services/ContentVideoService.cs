using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Service.DTOs.ContentVideos;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class ContentVideoService : IContentVideoService
{
	private readonly IMapper mapper;
	private readonly IRepository<ContentCategory> categoryRepository;
	private readonly IRepository<ContentVideo> contentVideoRepository;
	public ContentVideoService(
		IMapper mapper,
		IRepository<ContentCategory> categoryRepository,
		IRepository<ContentVideo> contentVideoRepository)
	{
		this.mapper = mapper;
		this.categoryRepository = categoryRepository;
		this.contentVideoRepository = contentVideoRepository;
	}

	public async ValueTask<ContentVideoResultDto> AddAsync(ContentVideoCreationDto dto)
	{
		var category = await this.categoryRepository.SelectAsync(c => c.Id.Equals(dto.CategoryId))
					  ?? throw new NotFoundException("This category is not found");

		var isChecked = YouTubeLinkValidator.IsValidYouTubeLink(dto.VideoLink);
		if (!isChecked)
			throw new NotFoundException("This link is not valid or does not macht with you tube link");

		var mappedVideo = this.mapper.Map<ContentVideo>(dto);
		mappedVideo.Category = category;
		await this.contentVideoRepository.InsertAsync(mappedVideo);
		await this.contentVideoRepository.SaveAsync();

		return this.mapper.Map<ContentVideoResultDto>(mappedVideo);
	}

	public async ValueTask<bool> RemoveAsync(long id)
	{
		var existVideo = await this.contentVideoRepository.SelectAsync(v => v.Id.Equals(id))
					?? throw new NotFoundException("This video is not found");

		this.contentVideoRepository.Delete(existVideo);
		await this.contentVideoRepository.SaveAsync();
		return true;
	}

	public async ValueTask<ContentVideoResultDto> RetrieveByIdAsync(long id)
	{
		var existVideo = await this.contentVideoRepository.SelectAsync(v => v.Id.Equals(id))
					?? throw new NotFoundException("This video is not found");

		return this.mapper.Map<ContentVideoResultDto>(existVideo);
	}

	public async ValueTask<IEnumerable<ContentVideoResultDto>> RetrieveAsync(PaginationParams @params, Filter filter, string search = null)
	{
		var videos = await this.contentVideoRepository.SelectAll()
			.ToPaginate(@params)
			.ToListAsync();

        if (!string.IsNullOrEmpty(search))
            videos = videos.Where(user => user.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        return this.mapper.Map<IEnumerable<ContentVideoResultDto>>(videos);
	}

	public async ValueTask<IEnumerable<ContentVideoResultDto>> RetrieveAllByCategoryIdAsync(long categoryId)
	{
		var content = await this.categoryRepository.SelectAsync(c => c.Id.Equals(categoryId))
			?? throw new NotFoundException("This category is not found");

		var videos = this.contentVideoRepository.SelectAll(c => c.CategoryId.Equals(categoryId));
		return this.mapper.Map<IEnumerable<ContentVideoResultDto>>(videos);
	}
}