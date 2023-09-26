using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.ContentVideos;

namespace Nabeey.Service.Interfaces;

public interface IContentVideoService
{
    ValueTask<ContentVideoResultDto> AddAsync(ContentVideoCreationDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<ContentVideoResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<ContentVideoResultDto>> RetrieveAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<IEnumerable<ContentVideoResultDto>> RetrieveAllByContentIdAsync(long contentId);
}