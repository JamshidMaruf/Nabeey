using AutoMapper;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Contents;
using Nabeey.DataAccess.IRepositories;
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

    public async ValueTask<BookResultDto> RetrieveByIdAsync(long id)
    {
        var book = await this.bookRepository.SelectAsync(b => b.Id.Equals(id))
            ?? throw new NotFoundException("This book is not found");
        
        return this.mapper.Map<BookResultDto>(book);   
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var book = await this.bookRepository.SelectAsync(b => b.Id.Equals(id))
            ?? throw new NotFoundException("This book is not found");

        this.bookRepository.Delete(book);
        await this.contentBookRepository.SaveAsync();
        return true;
    }

    public async ValueTask<BookResultDto> ModifyAsync(BookUpdateDto dto)
    {
        var book =await this.bookRepository.SelectAsync(b => b.Id.Equals(dto.Id))
            ?? throw new NotFoundException("This book is not found");

        var mapBook = this.mapper.Map(dto, book);
        this.bookRepository.Update(mapBook);
        await this.bookRepository.SaveAsync();

        return this.mapper.Map<BookResultDto>(mapBook);
    }

    public async ValueTask<IEnumerable<BookResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null)
    {
        var books = await this.bookRepository.SelectAll()
            .ToPaginate(@params)
            .ToListAsync();

        if(search is not null)
            books = books.Where(user => user.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
       
        return this.mapper.Map<IEnumerable<BookResultDto>>(books);
    }

    public async ValueTask<IEnumerable<BookResultDto>> RetrieveAllByContentIdAsync(long contentId)
    {
        var contents = await this.contentRepository.SelectAll().Where(q => q.ContentCategory.Id == contentId).ToListAsync()
            ?? throw new NotFoundException($"This quetionId:{contentId} is not found ");

        return this.mapper.Map<IEnumerable<BookResultDto>>(contents);
    }
}
