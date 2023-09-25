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

    public async Task<BookResultDto> UpdateAsync(BookUpdateDto dto)
    {
        Book existBook = this.repository.SelectAll().FirstOrDefault(b => b.Id.Equals(dto.Id));
        if (existBook is not null)
            throw new NotFoundException($"This id is not found {dto.Id}");

        this.mapper.Map(dto, existBook);
        this.repository.Update(existBook);
        await this.repository.SaveAsync();
        return this.mapper.Map<BookResultDto>(existBook);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        Book existBook = this.repository.SelectAll().FirstOrDefault(b => b.Id.Equals(id));
        if (existBook is not null)
            throw new NotFoundException($"This id is not found {id}");
        
        this.repository.Delete(existBook);
        await this.repository.SaveAsync();
        return true;
    }

    public async Task<BookResultDto> GetByIdAsync(long id)
    {
        Book existBook = await this.repository.SelectAll().FirstOrDefaultAsync(b => b.Id.Equals(id));
        if (existBook is not null)
            throw new NotFoundException($"This id is not found {id}");

        BookResultDto mappedBook = this.mapper.Map<BookResultDto>(existBook);
        return mappedBook;
    }

    public async Task<IEnumerable<BookResultDto>> GetAllAsync(PaginationParams @params, Filter filter,  string search = null)
    {
        var books = await this.repository.SelectAll()
                    .ToPaginate(@params)
                    .OrderBy(filter)
                    .ToListAsync();

        if (search is not null)
        {
            books = books.Where(book => book.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<BookResultDto>>(books);
    }
}
