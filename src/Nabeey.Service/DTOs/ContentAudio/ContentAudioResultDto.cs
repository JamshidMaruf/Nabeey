using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Contents;

namespace Nabeey.Service.DTOs.ContentAudio;

public class ContentAudioResultDto
{
    public ContentResultDto Content { get; set; }
    public AssetResultDto Asset { get; set; }
}