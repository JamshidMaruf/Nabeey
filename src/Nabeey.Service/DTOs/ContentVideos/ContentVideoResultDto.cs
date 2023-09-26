using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Contents;

namespace Nabeey.Service.DTOs.ContentVideos;

public class ContentVideoResultDto
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string Discription { get; set; }
	public ContentResultDto Content { get; set; }
	public AssetResultDto Video { get; set; }

  public string VideoPath { get; set; }
}