using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.ContentAudios;

namespace Nabeey.Service.Interfaces;

public interface IContentAudioService
{
    ValueTask<ContentAudioResultDto> AddAsync(ContentAudioCreationDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<ContentAudioResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<ContentAudioResultDto>> RetrieveAsync(PaginationParams @params, Filter filter, string search);
}