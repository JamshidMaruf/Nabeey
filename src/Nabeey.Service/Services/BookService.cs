using AutoMapper;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Books;
using Nabeey.DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Nabeey.Service.Services;

public class BookService : IBookService
{
    private readonly IMapper mapper;
    private readonly IRepository<Book> repository;

    public BookService(IMapper mapper, IRepository<Book> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public ValueTask<BookResultDto> AddAsync(BookCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<BookResultDto> ModifyAsync(BookUpdateDto dto)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<BookResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<BookResultDto>> RetrieveAllByContentIdAsync(long contentId)
    {
        throw new NotImplementedException();
    }

    public ValueTask<BookResultDto> RetrieveByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
