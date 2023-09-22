using Nabeey.Domain.Entities.Assets;
using Nabeey.Service.DTOs.Assets;

namespace Nabeey.Service.Interfaces;

public interface IAssetService
{
    Task<Asset> UploadAsync(AssetCreationDto dto);
    Task<bool> RemoveAsync(Asset asset);
}