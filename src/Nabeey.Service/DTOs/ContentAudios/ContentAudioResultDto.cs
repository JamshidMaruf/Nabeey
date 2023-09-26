using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Contents;

namespace Nabeey.Service.DTOs.ContentAudios;

public class ContentAudioResultDto
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public ContentResultDto Content { get; set; }
	public AssetResultDto Audio { get; set; }
}