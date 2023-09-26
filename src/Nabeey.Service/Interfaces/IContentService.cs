﻿using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Contents;

namespace Nabeey.Service.Interfaces;

public interface IContentService
{
	ValueTask<ContentResultDto> RetrieveByIdAsync(long id);
	ValueTask<IEnumerable<ContentResultDto>> RetrieveAllAsnyc(PaginationParams @params, Filter filter, string search = null);
}