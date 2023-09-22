using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.Assets;

public class AssetCreationDto
{
    public IFormFile FormFile { get; set; }
}