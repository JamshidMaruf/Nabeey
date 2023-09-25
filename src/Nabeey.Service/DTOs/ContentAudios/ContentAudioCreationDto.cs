using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.ContentAudios;

public class ContentAudioCreationDto
{
	public string Title { get; set; }
	public string Description { get; set; }
	public long ContentId { get; set; }
	public IFormFile Audio { get; set; }
}