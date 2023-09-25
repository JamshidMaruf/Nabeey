using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.ContentAudio;

public class ContentAudioCreationDto
{
	public string Title { get; set; }
	public string Description { get; set; }
	public long ContentId { get; set; }
	public IFormFile Audio { get; set; }
}