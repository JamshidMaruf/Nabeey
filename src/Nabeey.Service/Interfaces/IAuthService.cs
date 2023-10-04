using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.Users;
using Nabeey.WebApi.Models;

namespace Nabeey.Service.Interfaces;

public interface IAuthService
{
	ValueTask<UserResponseDto> GenerateTokenAsync(string phone, string originalPassword);
}
