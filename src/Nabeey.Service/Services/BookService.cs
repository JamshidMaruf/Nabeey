using AutoMapper;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Books;
using Nabeey.DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Contents;
using Nabeey.Domain.Entities.Contexts;

namespace Nabeey.Service.Services;

public class BookService : IBookService
{
    private readonly IMapper mapper;
    private readonly IRepository<Book> bookRepository;
    private readonly IRepository<Content> contentRepository;
    private readonly IRepository<ContentBook> contentBookRepository;

    public BookService(IMapper mapper, IRepository<Book> bookRepository, IRepository<ContentBook> contentBookRepository)
    {
        this.mapper = mapper;
        this.bookRepository = bookRepository;
        this.contentBookRepository = contentBookRepository;
    }

    public async ValueTask<BookResultDto> AddAsync(BookCreationDto dto)
    {
        var content = await this.contentRepository.SelectAsync(content => content.Id.Equals(dto.ContentId))
            ?? throw new NotFoundException("Content is not found");

        var mappedBook = this.mapper.Map<Book>(dto);
        await this.bookRepository.InsertAsync(mappedBook);

        var contentBook = new ContentBook
        {
            BookId = mappedBook.Id,
            ContentId = dto.ContentId
        };
        await this.contentBookRepository.InsertAsync(contentBook);
        await this.contentBookRepository.SaveAsync();

        return this.mapper.Map<BookResultDto>(mappedBook);
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
