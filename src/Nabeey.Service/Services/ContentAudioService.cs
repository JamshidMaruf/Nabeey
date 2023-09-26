using AutoMapper;
using Nabeey.Domain.Enums;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Service.DTOs.ContentAudios;

namespace Nabeey.Service.Services;

public class ContentAudioService : IContentAudioService
{
    private readonly IMapper mapper;
    private readonly IRepository<Content> contentRepository;
    private readonly IRepository<ContentAudio> contentAudioRepository;
    private readonly IAssetService assetService;
    public ContentAudioService(
        IMapper mapper,
        IAssetService assetService,
        IRepository<Content> contentRepository,
        IRepository<ContentAudio> contentAudioRepository)
    {
        this.mapper = mapper;
        this.assetService = assetService;
        this.contentRepository = contentRepository;
        this.contentAudioRepository = contentAudioRepository;
    }

    public async ValueTask<ContentAudioResultDto> AddAsync(ContentAudioCreationDto dto)
    {
        var content = await this.contentRepository.SelectAsync(c => c.Id.Equals(dto.ContentId))
                      ?? throw new NotFoundException("This content is not found");

        if (dto.Audio is null)
            throw new NotFoundException("This audio is not found");

        AssetCreationDto asset = new AssetCreationDto { FormFile = dto.Audio };

        var audio = await this.assetService.UploadAsync(asset, UploadType.Audios);
        var mappedAudio = this.mapper.Map<ContentAudio>(dto);

        mappedAudio.Audio = audio;
        mappedAudio.Content = content;

        await this.contentAudioRepository.InsertAsync(mappedAudio);
        await this.contentAudioRepository.SaveAsync();

        return this.mapper.Map<ContentAudioResultDto>(mappedAudio);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var existAudio = await this.contentAudioRepository.SelectAsync(a => a.Id.Equals(id))
                        ?? throw new NotFoundException("This content audio is not found");

        var isChecked = await this.assetService.RemoveAsync(existAudio.Audio);
        this.contentAudioRepository.Delete(existAudio);
        await this.contentAudioRepository.SaveAsync();
        return true;
    }

    public async ValueTask<ContentAudioResultDto> RetrieveByIdAsync(long id)
    {
        var existAudio =
            await this.contentAudioRepository.SelectAsync(a => a.Id.Equals(id), includes: new[] { "Content", "Audio" })
            ?? throw new NotFoundException("This content audio is not found");

        return this.mapper.Map<ContentAudioResultDto>(existAudio);
    }

    public async ValueTask<IEnumerable<ContentAudioResultDto>> RetrieveAsync(PaginationParams @params, Filter filter, string search)
    {
        var existAudios = await this.contentAudioRepository.SelectAll(includes: new[] { "Content", "Audio" })
            .ToPaginate(@params)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<ContentAudioResultDto>>(existAudios);
    }
}