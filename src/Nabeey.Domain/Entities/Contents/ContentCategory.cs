using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Entities.Books;

namespace Nabeey.Domain.Entities.Contexts;

public class ContentCategory : Auditable
{
	public string Name { get; set; }
	public string Description { get; set; }
	public long ImageId { get; set; }
	public Asset Image { get; set; } 
	public ICollection<Book> Books { get; set; }
	public ICollection<ContentAudio> Audios { get; set; }
	public ICollection<ContentVideo> Videos { get; set; }
	public ICollection<Article> Articles { get; set; }
}