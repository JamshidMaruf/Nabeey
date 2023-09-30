using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class AuthController : BaseController
{
	private readonly IAuthService authService;
	public AuthController(IAuthService authService)
	{
		this.authService = authService;
	}

	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<IActionResult> GenerateTokenAsync(string phone, string password)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.authService.GenerateTokenAsync(phone, password)
		});
}
