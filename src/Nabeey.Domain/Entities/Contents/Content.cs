using Nabeey.Domain.Commons;
using Nabeey.Domain.Entities.Articles;
using Nabeey.Domain.Entities.Books;
using Nabeey.Domain.Entities.Contents;
using System.Collections;

namespace Nabeey.Domain.Entities.Contexts;

public class Content : Auditable
{
    public long ContentCategoryId { get; set; }
    public ContentCategory ContentCategory { get; set; }
    public ICollection<ContentBook> Books { get; set; }
	public ICollection<ContentAudio> Audios { get; set; }
	public ICollection<ContentVideo> Videos { get; set; }
	public ICollection<Article> Articles { get; set; }
}