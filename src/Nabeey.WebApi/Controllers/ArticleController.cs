using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Articles;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

[Authorize]
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


	[HttpGet("get/{id:long}")]
	public async Task<IActionResult> GetAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.RetrieveAsync(id)
		});


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

    [HttpGet("get-by-user/{userId:long}")]
    public async Task<IActionResult> GetAllByUserIdAsync(long userId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RetrieveAllByUserIdAsync(userId)
        });
	
	[HttpGet("get-by-category/{categoryId:long}")]
    public async Task<IActionResult> GetAllByCategoryIdAsync(long categoryId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RetrieveAllByCategoryIdAsync(categoryId)
        });
}