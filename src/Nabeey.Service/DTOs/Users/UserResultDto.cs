using Nabeey.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.DTOs.Users;

public class UserResultDto
{
	public long Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public Role UserRole { get; set; }
	public IFormFile Image { get; set; }
}
