using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Books;

namespace Nabeey.Service.Interfaces;

public interface IBookService
{
    ValueTask<BookResultDto> AddAsync(BookCreationDto dto);
    ValueTask<BookResultDto> ModifyAsync(BookUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<BookResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<BookResultDto>> RetrieveAllAsync(PaginationParams @params,Filter filter, string search = null);
    ValueTask<IEnumerable<BookResultDto>> RetrieveAllByContentIdAsync(long contentId);
}
