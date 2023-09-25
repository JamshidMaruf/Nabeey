using Nabeey.Service.DTOs.Assets;

namespace Nabeey.Service.DTOs.ContentAudios;

public class ContentAudioResultDto
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public ContentAudioResultDto Content { get; set; }
	public AssetResultDto Audio { get; set; }
}