using Nabeey.Domain.Entities.Assets;
using Nabeey.Service.DTOs.Assets;

namespace Nabeey.Service.Interfaces;

public interface IAssetService
{
    ValueTask<Asset> UploadAsync(AssetCreationDto dto);
    ValueTask<bool> RemoveAsync(Asset asset);
}