using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Web.Models;

public class UserResponseDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Role UserRole { get; set; }
    public AssetResultDto Asset { get; set; }
    public string Token { get; set; }
}
