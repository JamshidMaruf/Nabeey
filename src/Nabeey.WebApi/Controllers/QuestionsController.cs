using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.Interfaces;
using Nabeey.Web.Models;

namespace Nabeey.Web.Controllers;

public class QuestionsController : BaseController
{
	private readonly IQuestionService questionService;
	public QuestionsController(IQuestionService questionService)
	{
		this.questionService = questionService;
	}

	[HttpPost("create")]
	public async ValueTask<IActionResult> PostAsync([FromForm] QuestionCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.questionService.AddAsync(dto)
		});

	[HttpPut("update")]
	public async ValueTask<IActionResult> UpdateAsync([FromForm] QuestionUpdateDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.questionService.ModifyAsync(dto)
		});

	[HttpDelete("delete/{id:long}")]
	public async ValueTask<IActionResult> DeleteAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.questionService.RemoveAsync(id)
		});

	[AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async ValueTask<IActionResult> GetAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.questionService.RetrieveByIdAsync(id)
		});

	[AllowAnonymous]
	[HttpGet("get-all")]
	public async ValueTask<IActionResult> GetAllAsync(
		[FromQuery] PaginationParams @params,
		[FromQuery] Filter filter, string search)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.questionService.RetrieveAllAsync(@params, filter, search)
		});
}
