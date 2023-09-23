using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.ContentVideos;

namespace Nabeey.Service.Interfaces;

public interface IContentVideoService
{
    Task<ContentVideoResultDto> AddAsync(ContentVideoCreationDto dto);
    Task<bool> RemoveAsync(long id);
    Task<ContentVideoResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<ContentVideoResultDto>> RetrieveAsync(PaginationParams @params, Filter filter, string search = null);
}