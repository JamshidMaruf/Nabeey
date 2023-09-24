using Nabeey.Domain.Configurations;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Service.Interfaces;

public interface IUserService
{
    Task<UserResultDto> AddAsync(UserCreationDto dto);
    Task<UserResultDto> ModifyAsync(UserUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<UserResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<UserResultDto>> RetrieveAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<UserResultDto>> RetrieveAllAsync();
    Task<UserResultDto> UpgradeRoleAsync(long id, Role role);
}
