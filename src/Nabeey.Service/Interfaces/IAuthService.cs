using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.Users;
using Nabeey.Web.Models;

namespace Nabeey.Service.Interfaces;

public interface IAuthService
{
	ValueTask<UserResponseDto> GenerateTokenAsync(string phone, string originalPassword);
}
