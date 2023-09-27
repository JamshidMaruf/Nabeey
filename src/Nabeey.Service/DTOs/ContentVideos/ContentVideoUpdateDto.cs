using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.ContentVideos;

public class ContentVideoUpdateDto
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string Discription { get; set; }
    public string VideoLink { get; set; }
	public long ContentId { get; set; }
}