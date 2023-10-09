using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.DTOs.QuestionAnswers;
using Nabeey.Service.Interfaces;
using Nabeey.Web.Models;

namespace Nabeey.Web.Controllers;

public class QuestionAnswerController : BaseController
{
	private readonly IQuestionAnswerService service;
	public QuestionAnswerController(IQuestionAnswerService service)
	{
		this.service = service;
	}

	[HttpPost("create")]
	public async ValueTask<IActionResult> PostAsync(QuestionAnswerCreationDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.AddAsync(dto)
		});

	[HttpPut("update")]
	public async ValueTask<IActionResult> UpdateAsync(QuestionAnswerUpdateDto dto)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.ModifyAsync(dto)
		});
}