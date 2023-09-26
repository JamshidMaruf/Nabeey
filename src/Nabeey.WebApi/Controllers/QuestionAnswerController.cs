using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.QuizQuestions;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class QuestionAnswerController : BaseController
{
    private readonly IQuizQuestionService service;
    public QuestionAnswerController(IQuizQuestionService service)
    {
        this.service = service;
    }

    [HttpPost("create")]
    public async ValueTask<IActionResult> PostAsync(QuizQuestionCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.AddAsync(dto)
        });

    [HttpPut("update")]
    public async ValueTask<IActionResult> UpdateAsync(QuizQuestionUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.ModifyAsync(dto)
        });
}
