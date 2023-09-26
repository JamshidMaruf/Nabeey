using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Contexts;
using Nabeey.Service.DTOs.ContentCategories;
using Nabeey.Service.DTOs.Contents;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class ContentCategoryService : IContentCategoryService
{
    private readonly IMapper mapper;
    private readonly IRepository<ContentCategory> repository;
    public ContentCategoryService(IRepository<ContentCategory> repository, IMapper mapper)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async ValueTask<ContentCategoryResultDto> AddAsync(ContentCategoryCreationDto dto)
    {
        var category = await this.repository.SelectAsync(c => c.Name.ToLower().Equals(dto.Name.ToLower()));
        if (category is not null)
            throw new AlreadyExistException("This category is already exists");

        var mappedCategory = this.mapper.Map<ContentCategory>(dto);
        await this.repository.InsertAsync(mappedCategory);
        await this.repository.SaveAsync();

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

    public async ValueTask<IEnumerable<ContentCategoryResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var categories = await this.repository.SelectAll().ToListAsync();
        return this.mapper.Map<IEnumerable<ContentCategoryResultDto>>(categories);
    }

    ValueTask<IEnumerable<ContentResultDto>> IContentCategoryService.RetrieveAllAsync(PaginationParams @params, Filter filter, string search)
    {
        throw new NotImplementedException();
    }
}
