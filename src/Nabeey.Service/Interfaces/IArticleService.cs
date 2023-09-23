using Nabeey.Service.DTOs.Articles;

namespace Nabeey.Service.Interfaces;

public interface IArticleService
{
    Task<ArticleResultDto> AddAsync(ArticleCreationDto dto);
    Task<ArticleResultDto> ModifyAsync(ArticleUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<ArticleResultDto> RetrieveAsync(long id);
    Task<IEnumerable<ArticleResultDto>> RetrieveAllAsync();
}
