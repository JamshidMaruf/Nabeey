using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.ContentBooks;

namespace Nabeey.Service.Interfaces;

public interface IContentBookService
{
    Task<ContentBookResultDto> AddAsync(ContentBookCreationDto dto);
    Task<ContentBookResultDto> UpdateAsync(ContentBookUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<ContentBookResultDto> GetByIdAsync(long id);
    Task<IEnumerable<ContentBookResultDto>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
