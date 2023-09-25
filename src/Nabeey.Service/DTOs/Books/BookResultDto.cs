using Nabeey.Domain.Entities.Assets;
using Nabeey.Service.DTOs.Assets;

namespace Nabeey.Service.DTOs.Books;

public class BookResultDto
{
    public long Id { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public string Description { get; set; }
	public AssetResultDto File { get; set; }
	public AssetResultDto Image { get; set; }
}
