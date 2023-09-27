using AutoMapper;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Interfaces;
using Nabeey.Service.Extensions;
using Nabeey.Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Service.DTOs.ContentCategories;

namespace Nabeey.Service.Services;

public class ContentCategoryService : IContentCategoryService
{
    private readonly IMapper mapper;
    private readonly IRepository<Content> contentRepository;
    private readonly IRepository<ContentCategory> repository;

    public ContentCategoryService(IRepository<ContentCategory> repository, IMapper mapper, IRepository<Content> contentRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.contentRepository = contentRepository;
    }

    public async ValueTask<ContentCategoryResultDto> AddAsync(ContentCategoryCreationDto dto)
    {
        var category = await this.repository.SelectAsync(c => c.Name.ToLower().Equals(dto.Name.ToLower()));
        if (category is not null)
            throw new AlreadyExistException("This category is already exists");

        var mappedCategory = this.mapper.Map<ContentCategory>(dto);
        await this.repository.InsertAsync(mappedCategory);
        await this.repository.SaveAsync();

        var content = new Content { ContentCategoryId = mappedCategory.Id };
        await this.contentRepository.InsertAsync(content);
        await this.contentRepository.SaveAsync();
        return this.mapper.Map<ContentCategoryResultDto>(mappedCategory);
    }

    public async ValueTask<ContentCategoryResultDto> ModifyAsync(ContentCategoryUpdateDto dto)
    {
        var category = await this.repository.SelectAsync(c => c.Id.Equals(dto.Id))
            ?? throw new NotFoundException("This category is not found");

        if (!category.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase))
        {
            var existCategory = await this.repository.SelectAsync(c => c.Name.ToLower().Equals(dto.Name.ToLower()));
            if (existCategory is not null)
                throw new AlreadyExistException("This category is already exists");
        }

        var mappedCategory = this.mapper.Map(dto, category);
        this.repository.Update(mappedCategory);
        await this.repository.SaveAsync();

        return this.mapper.Map<ContentCategoryResultDto>(mappedCategory);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var category = await this.repository.SelectAsync(c => c.Id.Equals(id))
            ?? throw new NotFoundException("This category is not found");

        this.repository.Delete(category);
        await this.repository.SaveAsync();
        return true;
    }

    public async ValueTask<ContentCategoryResultDto> RetrieveByIdAsync(long id)
    {
        var category = await this.repository.SelectAsync(c => c.Id.Equals(id))
            ?? throw new NotFoundException("This category is not found");

        return this.mapper.Map<ContentCategoryResultDto>(category);
    }

    public async ValueTask<IEnumerable<ContentCategoryResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null)
    {
        var categories = await this.repository.SelectAll()
                                              .ToPaginate(@params)
                                              .ToListAsync();
        if (search is not null)
            categories = categories.Where(category => category.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        return this.mapper.Map<IEnumerable<ContentCategoryResultDto>>(categories);
    }
}
