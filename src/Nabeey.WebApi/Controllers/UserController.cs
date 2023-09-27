using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Users;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class UserController : BaseController
{
	private readonly IUserService userService;
	public UserController(IUserService userService)
	{
		this.userService = userService;
	}

	[HttpPost("create")]
	public async ValueTask<IActionResult> PostAsync([FromForm] UserCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.AddAsync(dto)
		});

	[HttpPut("update")]
	public async ValueTask<IActionResult> PutAsync([FromForm] UserUpdateDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.ModifyAsync(dto)
		});

	[HttpDelete("delete/{id:long}")]
	public async ValueTask<IActionResult> DeleteAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.RemoveAsync(id)
		});

	[HttpGet("get/{id:long}")]
	public async ValueTask<IActionResult> GetByIdAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.RetrieveByIdAsync(id)
		});

	[HttpGet("get-all")]
	public async ValueTask<IActionResult> GetAllAsync(
		[FromQuery] PaginationParams @params,
		[FromQuery] string search)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.RetrieveAllAsync(@params, search)
		});

	[HttpPatch("upgrade-role")]
	public async ValueTask<IActionResult> UpgradeRoleAsync(long id, Role role)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.UpgradeRoleAsync(id, role)
		});
}
