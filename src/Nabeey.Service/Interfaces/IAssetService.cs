using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Domain.Entities.Assets;

namespace Nabeey.Service.Interfaces;

public interface IAssetService
{
    ValueTask<Asset> UploadAsync(AssetCreationDto dto, UploadType type);
    ValueTask<bool> RemoveAsync(Asset asset);
}