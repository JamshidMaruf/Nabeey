using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.ContentVideos;

public class ContentVideoCreationDto
{
	public string Title { get; set; }
	public string Description { get; set; }
    public string VideoLink { get; set; }
	public long ContentId { get; set; }
}