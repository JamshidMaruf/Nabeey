using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Contents;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class ContentBookService : IContentBookService
{
	private readonly IMapper mapper;
	private readonly IRepository<Book> bookRepository;
	private readonly IRepository<Content> contentRepository;
	private readonly IRepository<ContentBook> contentBookRepository;

	public ContentBookService(IMapper mapper, IRepository<Book> bookRepository, IRepository<Content> contentRepository, IRepository<ContentBook> contentBookRepository)
	{
		this.mapper = mapper;
		this.bookRepository = bookRepository;
		this.contentRepository = contentRepository;
		this.contentBookRepository = contentBookRepository;
	}
	public async Task<ContentBookResultDto> AddAsync(ContentBookCreationDto dto)
	{
		Book book = this.bookRepository.SelectAll().FirstOrDefault(c => c.Id.Equals(dto.BookId));
		if (book is not null)
			throw new NotFoundException($"This bookId is not found {dto.BookId}");

		Content content = this.contentRepository.SelectAll().FirstOrDefault(c => c.Id.Equals(dto.ContentId));
		if (content is not null)
			throw new NotFoundException($"This contentId is not found{dto.ContentId}");

		ContentBook contentBook = this.mapper.Map<ContentBook>(dto);
		ContentBookResultDto result = this.mapper.Map<ContentBookResultDto>(contentBook);
		await this.contentBookRepository.SaveAsync();
		return result;
	}

	public async Task<ContentBookResultDto> UpdateAsync(ContentBookUpdateDto dto)
	{
		ContentBook contentBook = await this.contentBookRepository.SelectAsync(c => c.Id.Equals(dto.Id))
			?? throw new NotFoundException($"This ContentBookId is not found {dto.Id}");

		ContentBook mappedContentBook = this.mapper.Map(dto, contentBook);
		this.contentBookRepository.Update(mappedContentBook);
		await this.contentBookRepository.SaveAsync();

		return this.mapper.Map<ContentBookResultDto>(mappedContentBook);
	}

	public async Task<bool> DeleteAsync(long id)
	{
		ContentBook contentBook = await this.contentBookRepository.SelectAsync(c => c.Id.Equals(id))
			?? throw new NotFoundException($"This ContentBookId is not found {id}");

		this.contentBookRepository.Delete(contentBook);
		await this.contentBookRepository.SaveAsync();
		return true;
	}

	public async Task<ContentBookResultDto> GetByIdAsync(long id)
	{
		ContentBook contentBook = await this.contentBookRepository.SelectAsync(c => c.Id.Equals(id))
			?? throw new NotFoundException($"This ContentBookId is not found {id}");

		return this.mapper.Map<ContentBookResultDto>(contentBook);
	}

	public async ValueTask<IEnumerable<ContentBookResultDto>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
	{
		var contentBooks = (await this.contentBookRepository.SelectAll().ToListAsync())
																		.OrderBy(filter)
																		.ToPaginate(@params);

		if (search is not null)
			contentBooks = contentBooks.Where(contentBook => contentBook.Book.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

		return this.mapper.Map<IEnumerable<ContentBookResultDto>>(contentBooks);
	}
}
