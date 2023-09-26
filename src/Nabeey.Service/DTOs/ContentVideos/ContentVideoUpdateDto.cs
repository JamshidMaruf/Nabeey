using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.ContentVideos;

public class ContentVideoUpdateDto
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string Discription { get; set; }
	public IFormFile Video { get; set; }
	public long ContentId { get; set; }

    public string VideoPath { get; set; }
}