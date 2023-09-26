using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Assets;

namespace Nabeey.Service.Interfaces;

public interface IAssetService
{
    ValueTask<Asset> UploadAsync(AssetCreationDto dto, UploadType type);
    ValueTask<bool> RemoveAsync(Asset asset);
}