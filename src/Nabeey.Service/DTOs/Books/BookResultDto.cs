namespace Nabeey.Service.DTOs.Books;

public class BookResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public long AssetId { get; set; }
    public string Text { get; set; }
}
