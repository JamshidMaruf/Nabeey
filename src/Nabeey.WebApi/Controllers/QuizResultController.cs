using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class QuizResultController : BaseController
{
	private readonly IQuizResultService quizResultService;
	public QuizResultController(IQuizResultService quizResultService)
	{
		this.quizResultService = quizResultService;
	}

    [HttpGet("get-by-quizId-userId/{quizId:long}/{userId:long}")]
    public async ValueTask<IActionResult> GetAsync(long userId, long quizId)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.quizResultService.RetrieveByUserIdAsync(userId, quizId)
		});

	[HttpGet("get-by-quizId/{quizId:long}")]
	public async ValueTask<IActionResult> GetByQuizIdAsync(long quizId)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.quizResultService.RetrieveAllQuizIdAsync(quizId)
		});
}