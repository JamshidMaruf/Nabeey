using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.Assets;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.Interfaces;

namespace Nabeey.Service.Services;

public class AssetService : IAssetService
{
    private readonly IRepository<Asset> repository;

    public AssetService(IRepository<Asset> repository)
    {
        this.repository = repository;
    }

    public Task<bool> RemoveAsync(Asset asset)
    {
        throw new NotImplementedException();
    }

    public Task<Asset> UploadAsync(AssetCreationDto dto)
    {
        throw new NotImplementedException();
    }
}