using Microsoft.AspNetCore.Http;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Assets;

namespace Nabeey.Service.DTOs.Users;

public class UserResultDto
{
	public long Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public Role UserRole { get; set; }
	public AssetResultDto Asset { get; set; }
}
