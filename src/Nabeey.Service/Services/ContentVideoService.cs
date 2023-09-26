using AutoMapper;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Service.DTOs.ContentVideos;

namespace Nabeey.Service.Services;

public class ContentVideoService : IContentVideoService
{
    private readonly IMapper mapper;
    private readonly IRepository<Content> contentRepository;
    private readonly IRepository<ContentVideo> contentVideoRepository;
    public ContentVideoService(
        IMapper mapper, 
        IRepository<Content> contentRepository, 
        IRepository<ContentVideo> contentVideoRepository)
    {
        this.mapper = mapper;
        this.contentRepository = contentRepository;
        this.contentVideoRepository = contentVideoRepository;
    }

    public async ValueTask<ContentVideoResultDto> AddAsync(ContentVideoCreationDto dto)
    {
        var content = await this.contentRepository.SelectAsync(c => c.Id.Equals(dto.ContentId))
                      ?? throw new NotFoundException("This content is not found");

        var isChecked = YouTubeLinkValidator.IsValidYouTubeLink(dto.VideoPath);
        if (!isChecked)
            throw new NotFoundException("This video is not found");

        var mappedVideo = this.mapper.Map<ContentVideo>(dto);
        mappedVideo.Content = content;
        await this.contentVideoRepository.InsertAsync(mappedVideo);
        await this.contentVideoRepository.SaveAsync();

        return this.mapper.Map<ContentVideoResultDto>(mappedVideo);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var existVideo = await this.contentVideoRepository.SelectAsync(v => v.Id.Equals(id))
                    ?? throw new NotFoundException("This content video is not found");
        
        this.contentVideoRepository.Delete(existVideo);
        await this.contentVideoRepository.SaveAsync();
        return true;
    }

    public async ValueTask<ContentVideoResultDto> RetrieveByIdAsync(long id)
    {
        var existVideo = await this.contentVideoRepository.SelectAsync(v => v.Id.Equals(id), includes: new[] {"Content"})
                    ?? throw new NotFoundException("This content video is not found");

        return this.mapper.Map<ContentVideoResultDto>(existVideo);
    }

    public async ValueTask<IEnumerable<ContentVideoResultDto>> RetrieveAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var videos = (await this.contentVideoRepository.SelectAll(includes: new[] { "Content" })
                                                            .ToListAsync())
                                                            .OrderBy(filter)
                                                            .ToPaginate(@params);

        return this.mapper.Map<IEnumerable<ContentVideoResultDto>>(videos);
    }

	public async ValueTask<IEnumerable<ContentVideoResultDto>> RetrieveAllByContentIdAsync(long contentId)
	{
        var content = await this.contentRepository.SelectAsync(c => c.Id.Equals(contentId))
            ?? throw new NotFoundException("This content is not found");

		var videos = this.contentVideoRepository.SelectAll(c => c.ContentId.Equals(contentId));
        return this.mapper.Map<IEnumerable<ContentVideoResultDto>>(videos);
	}
}