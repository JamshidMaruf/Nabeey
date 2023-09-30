using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class QuizResultController : BaseController
{
	private readonly IQuizResultService quizResultService;
	private readonly ICertificateService certificateService;
    private readonly IWebHostEnvironment webHostEnvironment;
    public QuizResultController(IQuizResultService quizResultService, ICertificateService certificateService, IWebHostEnvironment webHostEnvironment)
    {
        this.quizResultService = quizResultService;
        this.certificateService = certificateService;
        this.webHostEnvironment = webHostEnvironment;
    }

    [HttpGet("get/{id:long}")]
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

	[HttpGet("get-certificate")]
	public async ValueTask<IActionResult> GetCertificate(long userId, long quizId)
    {
        var dtos = await certificateService.RetrieveByQuizIdCertificateAsync(userId, quizId);

        foreach (var i in dtos)
            i.File.FilePath = Path.Combine(webHostEnvironment.WebRootPath, i.File.FilePath);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = dtos
        });
    }
}