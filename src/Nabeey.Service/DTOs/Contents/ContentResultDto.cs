using Nabeey.Service.DTOs.ContentCategories;

namespace Nabeey.Service.DTOs.Contents;

public class ContentResultDto
{
    public long Id { get; set; }
    public ContentCategoryResultDto ContentCategory { get; set; }
}