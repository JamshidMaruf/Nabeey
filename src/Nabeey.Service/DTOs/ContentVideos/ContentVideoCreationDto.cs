using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.ContentVideos;

public class ContentVideoCreationDto
{
	public string Title { get; set; }
	public string Discription { get; set; }
	public IFormFile Video { get; set; }
	public long ContentId { get; set; }
}