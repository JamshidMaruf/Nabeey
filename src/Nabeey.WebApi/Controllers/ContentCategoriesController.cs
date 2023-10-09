﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.ContentCategories;
using Nabeey.Service.Interfaces;
using Nabeey.Web.Models;

namespace Nabeey.Web.Controllers;

public class ContentCategoriesController : BaseController
{
	private readonly IContentCategoryService contentCategoryService;
	public ContentCategoriesController(IContentCategoryService contentCategoryService)
	{
		this.contentCategoryService = contentCategoryService;
	}

	[HttpPost("create")]
	public async ValueTask<IActionResult> PostAsync([FromForm] ContentCategoryCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.contentCategoryService.AddAsync(dto)
		});


	[HttpPut("update")]
	public async ValueTask<IActionResult> PutAsync([FromForm] ContentCategoryUpdateDto dto)
	   => Ok(new Response
	   {
		   StatusCode = 200,
		   Message = "Success",
		   Data = await this.contentCategoryService.ModifyAsync(dto)
	   });


	[HttpDelete("delete/{id:long}")]
	public async ValueTask<IActionResult> DeleteAsync(long id)
	   => Ok(new Response
	   {
		   StatusCode = 200,
		   Message = "Success",
		   Data = await this.contentCategoryService.RemoveAsync(id)
	   });


	[AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async ValueTask<IActionResult> GetByIdAsync(long id)
	   => Ok(new Response
	   {
		   StatusCode = 200,
		   Message = "Success",
		   Data = await this.contentCategoryService.RetrieveByIdAsync(id)
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
		Data = await this.contentCategoryService.RetrieveAllAsync(@params, search)
	});
}
