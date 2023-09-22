using AutoMapper;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Books;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Interfaces;

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

    public async Task<BookResultDto> AddAsync(BookCreationDto dto)
    {
        Book existBook = this.repository.SelectAll().FirstOrDefault(b=>b.Title.Equals(dto.Title));
        if (existBook is not null)
            throw new AlreadyExistException($"This title is already exist {dto.Title}");
        
        var mappedBook=this.mapper.Map<Book>(dto);
        await this.repository.CreateAsync(mappedBook);
        await this.repository.SaveAsync();

        return this.mapper.Map<BookResultDto>(mappedBook);
    }

    public Task<BookResultDto> UpdateAsync(BookUpdateDto dto)
    {
        Book existBook = this.repository.SelectAll().FirstOrDefault(b => b.Title.Equals(dto.Title));
        if (existBook is not null)
            throw new AlreadyExistException($"This title is already exist {dto.Title}");
    }

    public Task<bool> DeleteAsync(long id)
    {

        throw new NotImplementedException();
    }

    public Task<BookResultDto> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookResultDto>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        throw new NotImplementedException();
    }
}
