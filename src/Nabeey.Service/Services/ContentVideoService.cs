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

    public async Task<ContentVideoResultDto> AddAsync(ContentVideoCreationDto dto)
    {
        var content = await this.contentRepository
                    .SelectAsync(expression: content => content.Id.Equals(dto.ContentId))
                    ?? throw new NotFoundException($"This content is not found with ID: {dto.ContentId}");

        var mappedContentVideo = this.mapper.Map<ContentVideo>(dto);
        await this.contentVideoRepository.InsertAsync(mappedContentVideo);
        await this.contentVideoRepository.SaveAsync();

        return this.mapper.Map<ContentVideoResultDto>(mappedContentVideo);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var contentVideo = await this.contentVideoRepository.SelectAsync(expression: cv => cv.Id.Equals(id))
                            ?? throw new NotFoundException($"This content video is not found with ID: {id}");

        this.contentVideoRepository.Delete(contentVideo);
        await this.contentVideoRepository.SaveAsync();
        return true;
    }

    public async Task<ContentVideoResultDto> RetrieveByIdAsync(long id)
    {
        var contentVideo = 
            await this.contentVideoRepository.SelectAsync(expression: cv => cv.Id.Equals(id) , includes: new[] { "Content", "Asset" })
            ?? throw new NotFoundException($"This content video is not found with ID: {id}");

        return this.mapper.Map<ContentVideoResultDto>(contentVideo);
    }

    public async Task<IEnumerable<ContentVideoResultDto>> RetrieveAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var contentVideos = (await this.contentVideoRepository.SelectAll(includes: new[] { "Content", "Asset" })
                                                                .ToListAsync())
                                                                .OrderBy(filter)
                                                                .ToPaginate(@params);

        return this.mapper.Map<IEnumerable<ContentVideoResultDto>>(contentVideos);
    }
}