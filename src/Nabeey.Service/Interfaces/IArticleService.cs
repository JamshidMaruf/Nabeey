using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Articles;

namespace Nabeey.Service.Interfaces;

public interface IArticleService
{
	ValueTask<ArticleResultDto> AddAsync(ArticleCreationDto dto);
	ValueTask<ArticleResultDto> ModifyAsync(ArticleUpdateDto dto);
	ValueTask<bool> DeleteAsync(long id);
	ValueTask<ArticleResultDto> RetrieveAsync(long id);
	ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllByUserIdAsync(long userId);
	ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllByCategoryIdAsync(long categoryId);
	ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null);
}
