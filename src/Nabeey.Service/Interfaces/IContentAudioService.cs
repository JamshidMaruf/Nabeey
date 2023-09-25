using Nabeey.Domain.Entities.Contexts;
using Nabeey.Service.DTOs.ContentAudios;

namespace Nabeey.Service.Interfaces;

public interface IContentAudioService
{
    Task<ContentAudio> UploadAsync(ContentAudioCreationDto dto);
    Task<bool> RemoveAsync(long id);
    Task<ContentAudioResultDto> RetrieveByIdAsync(long id);
}