using AutoMapper;
using Nabeey.Service.Helpers;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using Nabeey.Service.Exceptions;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Service.DTOs.ContentAudio;

namespace Nabeey.Service.Services;

public class ContentAudioService : IContentAudioService
{
    private readonly IMapper mapper;
    private readonly IRepository<ContentAudio> contentAudioRepository;
    public ContentAudioService(IMapper mapper,IRepository<ContentAudio> contentAudioRepository)
    {
        this.mapper = mapper;
        this.contentAudioRepository = contentAudioRepository;
    }

    public async Task<ContentAudio> UploadAsync(ContentAudioCreationDto dto)
    {
        var webrootPath = Path.Combine(PathHelper.WebRootPath, "Audios");

        if (!Directory.Exists(webrootPath))
            Directory.CreateDirectory(webrootPath);

        var fileExtension = Path.GetExtension(dto.File.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtension}";
        var fullPath = Path.Combine(webrootPath, fileName);  

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(dto.File.ToByte());

        var createdContentAudio = new ContentAudio
        {
            AssetId = dto.AssetId,
            ContentId = dto.ContentId,
        };

        await this.contentAudioRepository.CreateAsync(createdContentAudio);
        await this.contentAudioRepository.SaveAsync();

        return createdContentAudio;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var exisContentAudio = await this.contentAudioRepository.SelectAsync(expression: cv => cv.Id.Equals(id))
                            ?? throw new NotFoundException("Not found");

        this.contentAudioRepository.Delete(exisContentAudio);
        await this.contentAudioRepository.SaveAsync();
        return true;
    }

    public async Task<ContentAudioResultDto> RetrieveByIdAsync(long id)
    {
        var contentAudio =
         await this.contentAudioRepository.SelectAsync(expression: cv => cv.Id.Equals(id), includes: new[] { "Content", "Asset" })
         ?? throw new NotFoundException($"This content audio is not found with ID: {id}");

        return this.mapper.Map<ContentAudioResultDto>(contentAudio);
    }
}