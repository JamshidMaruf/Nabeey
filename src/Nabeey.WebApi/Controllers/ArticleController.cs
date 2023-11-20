using Nabeey.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Articles;
using Microsoft.AspNetCore.Authorization;

namespace Nabeey.Web.Controllers;

public class ArticleController : BaseController
{
	private readonly IArticleService service;
	public ArticleController(IArticleService service)
	{
		this.service = service;
	}

	[HttpPost("create")]
	public async Task<IActionResult> PostAsync([FromForm] ArticleCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.AddAsync(dto)
		});


	[HttpPut("update")]
	public async Task<IActionResult> UpdateAsync([FromForm] ArticleUpdateDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.ModifyAsync(dto)
		});


	[HttpDelete("delete/{id:long}")]
	public async Task<IActionResult> DeleteAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.DeleteAsync(id)
		});


	[AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async Task<IActionResult> GetAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.RetrieveAsync(id)
		});


	[AllowAnonymous]
	[HttpGet("get-all")]
	public async Task<IActionResult> GetAllAsync(
		[FromQuery] PaginationParams @params,
		[FromQuery] Filter filter, string search)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.RetrieveAllAsync(@params, filter, search)
		});

	[AllowAnonymous]
	[HttpGet("get-by-user/{userId:long}")]
	public async Task<IActionResult> GetAllByUserIdAsync(long userId)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.RetrieveAllByUserIdAsync(userId)
		});

	[AllowAnonymous]
	[HttpGet("get-by-category/{categoryId:long}")]
	public async Task<IActionResult> GetAllByCategoryIdAsync(long categoryId)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.RetrieveAllByCategoryIdAsync(categoryId)
		});

	[AllowAnonymous]
    [HttpGet("files/{fileName}")]
	public IActionResult DownloadFile(string fileName)
	{
		var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
		var stream = new FileStream(path, FileMode.Open);
		var fileStreamResult = new FileStreamResult(stream, "application/octet-stream");
		fileStreamResult.FileDownloadName = fileName;
		return fileStreamResult;
	}
}