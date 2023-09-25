using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.Extensions;
using Nabeey.Service.Helpers;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class AssetService : IAssetService
{
    private readonly IRepository<Asset> repository;

    public AssetService(IRepository<Asset> repository)
    {
        this.repository = repository;
    }

    public async ValueTask<Asset> UploadAsync(AssetCreationDto dto, UploadType type)
    {
        var webRootPath = Path.Combine(PathHelper.WebRootPath, type.ToString());

        if (!Directory.Exists(webRootPath))
            Directory.CreateDirectory(webRootPath);

        var fileExtention = Path.GetExtension(dto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtention}";
        var filePath = Path.Combine(webRootPath, fileName);

        var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(dto.FormFile.ToByte());

        var asset = new Asset()
        {
            FileName = fileName,
            FilePath = filePath,
        };
        await this.repository.InsertAsync(asset);
        await this.repository.SaveAsync();
        return asset;
    }

    public async ValueTask<bool> RemoveAsync(Asset Assetment)
    {
        if (Assetment is null)
            return false;

        var existAssetment = await repository.SelectAsync(a => a.Id.Equals(Assetment.Id));

        if (existAssetment is null)
            return false;
        this.repository.Delete(existAssetment);
        var result = await this.repository.SaveAsync();
        return true;
    }
}