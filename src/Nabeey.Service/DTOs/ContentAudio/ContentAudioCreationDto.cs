using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.ContentAudio;

public class ContentAudioCreationDto
{
    public long ContentId { get; set; }
    public long AssetId { get; set; }
    public IFormFile File { get; set; }
}