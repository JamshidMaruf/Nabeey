﻿using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.ContentCategories;
using Nabeey.Service.DTOs.Contents;

namespace Nabeey.Service.Interfaces;

public interface IContentCategoryService
{
	ValueTask<ContentCategoryResultDto> AddAsync(ContentCategoryCreationDto dto);
	ValueTask<ContentCategoryResultDto> ModifyAsync(ContentCategoryUpdateDto dto);
	ValueTask<bool> RemoveAsync(long id);
	ValueTask<ContentCategoryResultDto> RetrieveByIdAsync(long id);
	ValueTask<IEnumerable<ContentCategoryResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null);
}
