using AutoMapper;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.Assets;

namespace Nabeey.Service.Services;

public class BookService : IBookService
{
	private readonly IMapper mapper;
	private readonly IAssetService assetService;
	private readonly IRepository<Book> bookRepository;
	private readonly IRepository<ContentCategory> categoryRepository;

	public BookService(IMapper mapper, IRepository<Book> bookRepository, IRepository<ContentCategory> categoryRepository, IAssetService assetService)
	{
		this.mapper = mapper;
		this.assetService = assetService;
		this.bookRepository = bookRepository;
		this.categoryRepository = categoryRepository;
	}

	public async ValueTask<BookResultDto> AddAsync(BookCreationDto dto)
	{
		var category = await this.categoryRepository.SelectAsync(content => content.Id.Equals(dto.CategoryId))
			?? throw new NotFoundException("Content is not found");

		var updloadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image }, UploadType.Images);
		var updloadedFile = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.File }, UploadType.Files);

		var mappedBook = new Book
		{
			FileId = updloadedFile.Id,
			File = updloadedFile,
			ImageId = updloadedImage.Id,
			Image = updloadedImage, 
			Title = dto.Title,
			Description = dto.Description,
			CategoryId = dto.CategoryId,
			Author = dto.Author,
			Category = category
		};
		await this.bookRepository.InsertAsync(mappedBook);
		await this.bookRepository.SaveAsync();

		return this.mapper.Map<BookResultDto>(mappedBook);
	}

	public async ValueTask<BookResultDto> RetrieveByIdAsync(long id)
	{
		var book = await this.bookRepository.SelectAsync(expression: b => b.Id.Equals(id), includes: new[] { "Image", "File" })
			?? throw new NotFoundException("This book is not found");

		return this.mapper.Map<BookResultDto>(book);
	}

	public async ValueTask<bool> DeleteAsync(long id)
	{
		var book = await this.bookRepository.SelectAsync(b => b.Id.Equals(id), includes: new[] { "Image", "File" })
			?? throw new NotFoundException("This book is not found");

		this.bookRepository.Delete(book);
		await this.assetService.RemoveAsync(book.Image);
        await this.assetService.RemoveAsync(book.File);
        await this.bookRepository.SaveAsync();
		return true;
	}

	public async ValueTask<BookResultDto> ModifyAsync(BookUpdateDto dto)
	{
		var book = await this.bookRepository.SelectAsync(expression: b => b.Id.Equals(dto.Id), includes: new[] { "Image", "File" })
			?? throw new NotFoundException("This book is not found");

		var updloadedImage = new Asset();
        if (dto.Image is not null)
		{
			await this.assetService.RemoveAsync(book.Image);
			updloadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image }, UploadType.Images);
		}

		var updloadedFile = new Asset();
        if (dto.File is not null)
		{
			await this.assetService.RemoveAsync(book.File);
			updloadedFile = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.File }, UploadType.Files);
        }

		if(updloadedImage.Id > 0)
		{
            book.Image ??= new Asset();
            
            book.ImageId = updloadedImage.Id;
            book.Image.FileName = updloadedImage.FileName;
            book.Image.FilePath = updloadedImage.FilePath;
        }

        if (updloadedFile.Id > 0)
        {
            book.File??= new Asset();

            book.FileId = updloadedFile.Id;
            book.File.FileName = updloadedImage.FileName;
            book.File.FilePath = updloadedImage.FilePath;
        }

        book.Title = dto.Title;
        book.Description = dto.Description;
        book.CategoryId = dto.CategoryId;
		book.Author = dto.Author;

        this.bookRepository.Update(book);
		await this.bookRepository.SaveAsync();

		return this.mapper.Map<BookResultDto>(book);
	}

	public async ValueTask<IEnumerable<BookResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null)
	{
		var books = await this.bookRepository.SelectAll(includes: new[] { "Image", "File" })
			.ToPaginate(@params)
			.ToListAsync();

		if (search is not null)
			books = books.Where(user => user.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

		return this.mapper.Map<IEnumerable<BookResultDto>>(books);
	}

	public async ValueTask<IEnumerable<BookResultDto>> RetrieveAllByCategoryIdAsync(long categoryId)
	{
		var books = await this.bookRepository.SelectAll(expression: q => q.CategoryId.Equals(categoryId), includes: new[] { "Image", "File"}).ToListAsync()
			?? throw new NotFoundException($"This quetionId:{categoryId} is not found ");
			
		return this.mapper.Map<IEnumerable<BookResultDto>>(books);
	}
}
