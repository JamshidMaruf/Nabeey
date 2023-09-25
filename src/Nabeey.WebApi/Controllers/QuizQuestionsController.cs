using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.DTOs.Quizzes.QuizQuestions;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class QuizQuestionsController : BaseController
{
    private readonly IQuizQuestionService service;
    public QuizQuestionsController(IQuizQuestionService service)
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

    [HttpDelete("delete/{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RemoveAsync(id)
        });

    [HttpGet("get/{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RetrieveAsync(id)
        });

    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RetrieveAllAsync()
        });

    [HttpGet("get-byQuiz")]
    public async ValueTask<IActionResult> GetByQuizAsync(long quizId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.service.RetrieveByQuiz(quizId)
        });
}