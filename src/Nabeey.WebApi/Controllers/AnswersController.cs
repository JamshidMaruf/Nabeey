using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Answers;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class AnswersController : BaseController
{
	private readonly IAnswerService answerService;
	public AnswersController(IAnswerService answerService)
	{
		this.answerService = answerService;
	}

	[HttpPost("create")]
	public async Task<IActionResult> PostAsync([FromQuery] AnswerCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.answerService.AddAsync(dto)
		});

	[HttpPut("update")]
	public async Task<IActionResult> UpdateAsync([FromQuery] AnswerUpdateDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.answerService.ModifyAsync(dto)
		});

	[HttpDelete("delete/{id:long}")]
	public async Task<IActionResult> DeleteAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.answerService.RemoveAsync(id)
		});

		[AllowAnonymous]
		[HttpGet("get/{id:long}")]
		public async Task<IActionResult> GetAsync(long id)
			=> Ok(new Response
			{
				StatusCode = 200,
				Message = "Success",
				Data = await this.answerService.RetrieveByIdAsync(id)
			});

		[AllowAnonymous]
		[HttpGet("get-all")]
		public async ValueTask<IActionResult> GetAllAsync(
			[FromQuery] PaginationParams @params)
			=> Ok(new Response
			{
				StatusCode = 200,
				Message = "Success",
				Data = await this.answerService.RetrieveAllAsync(@params)
			});

		[AllowAnonymous]
		[HttpGet("get-by-questionId/{questionId:long}")]
		public async ValueTask<IActionResult> GetAllByContentIdAsync(long questionId)
			=> Ok(new Response
			{
				StatusCode = 200,
				Message = "Success",
				Data = await this.answerService.RetrieveAllByQuestionIdAsync(questionId)
			});
}
