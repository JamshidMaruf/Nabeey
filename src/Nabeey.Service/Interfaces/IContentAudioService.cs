using Nabeey.Domain.Entities.Contexts;
using Nabeey.Service.DTOs.ContentAudios;

namespace Nabeey.Service.Interfaces;

public interface IContentAudioService
{
    ValueTask<ContentAudio> UploadAsync(ContentAudioCreationDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<ContentAudioResultDto> RetrieveByIdAsync(long id);
    ValueTask<ContentAudioResultDto> RetrieveByContentIdAsync(long contentId);
}