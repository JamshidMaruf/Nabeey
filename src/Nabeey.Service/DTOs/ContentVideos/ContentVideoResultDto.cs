using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Contents;

namespace Nabeey.Service.DTOs.ContentVideos;

public class ContentVideoResultDto
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
    public string VideoLink { get; set; }
    public ContentResultDto Content { get; set; }
}