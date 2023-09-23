using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Contents;

namespace Nabeey.Service.DTOs.ContentVideos;

public class ContentVideoResultDto
{
    public ContentResultDto Content { get; set; }
    public AssetResultDto Asset { get; set; }
}