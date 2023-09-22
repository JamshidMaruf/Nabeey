using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Contexts;

namespace Nabeey.Service.DTOs.Books;

public class BookCreationDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public long AssetId { get; set; }
    public string Text { get; set; }
}
