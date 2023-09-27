namespace Nabeey.Service.DTOs.ContentVideos;

public class ContentVideoCreationDto
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string VideoLink { get; set; }
	public long CategoryId { get; set; }
}