using Nabeey.Service.DTOs.Articles;

namespace Nabeey.Service.Interfaces;

public interface IArticleService
{
    ValueTask<ArticleResultDto> AddAsync(ArticleCreationDto dto);
    ValueTask<ArticleResultDto> ModifyAsync(ArticleUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<ArticleResultDto> RetrieveAsync(long id);
    ValueTask<IEnumerable<ArticleResultDto>> RetrieveAllAsync();
}
