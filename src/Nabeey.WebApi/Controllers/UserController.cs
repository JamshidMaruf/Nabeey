using Microsoft.AspNetCore.Authorization;
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
	private readonly ICertificateService certificateService;
    private readonly IWebHostEnvironment webHostEnvironment;
    public UserController(IUserService userService, ICertificateService certificateService, IWebHostEnvironment webHostEnvironment)
    {
        this.userService = userService;
        this.certificateService = certificateService;
        this.webHostEnvironment = webHostEnvironment;
    }

	[AllowAnonymous]
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

	[AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async ValueTask<IActionResult> GetByIdAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.userService.RetrieveByIdAsync(id)
		});

	[AllowAnonymous]
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


	[HttpGet("get-certificate")]
	public async ValueTask<IActionResult> GetCertificate(long userId)
	{
        var dtos = await certificateService.RetriveUserCertificatesAsync(userId);

		foreach(var i in dtos)
			i.File.FilePath = Path.Combine(webHostEnvironment.WebRootPath, i.File.FilePath);

		return Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = dtos
		});
    }
}
