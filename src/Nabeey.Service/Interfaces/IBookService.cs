using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Books;

namespace Nabeey.Service.Interfaces;

public interface IBookService
{
    Task<BookResultDto> AddAsync(BookCreationDto dto);
    Task<BookResultDto> UpdateAsync(BookUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<BookResultDto> GetByIdAsync(long id);
    ValueTask<IEnumerable<BookResultDto>> GetAllAsync(PaginationParams @params,Filter filter, string search = null);
}
