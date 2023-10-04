using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class QuizResultController : BaseController
{
	private readonly IQuizResultService quizResultService;
	private readonly ICertificateService certificateService;
    public QuizResultController(IQuizResultService quizResultService, ICertificateService certificateService)
    {
        this.quizResultService = quizResultService;
        this.certificateService = certificateService;
    }

	[AllowAnonymous]
    [HttpGet("get-by-quizId-userId/{quizId:long}/{userId:long}")]
    public async ValueTask<IActionResult> GetAsync(long userId, long quizId)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.quizResultService.RetrieveByUserIdAsync(userId, quizId)
		});


	[AllowAnonymous]
	[HttpGet("get-by-quizId/{quizId:long}")]
	public async ValueTask<IActionResult> GetByQuizIdAsync(long quizId)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.quizResultService.RetrieveAllQuizIdAsync(quizId)
		});

    [AllowAnonymous]
    [HttpGet("get-certificate/{quizId:long}")]
    public async ValueTask<IActionResult> GetCertificateAsync(long userId, long quizId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.certificateService.RetrieveByQuizIdCertificateAsync(userId, quizId)
        });
}