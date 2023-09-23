using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class QuizzesController : BaseController
{
    private readonly IQuizService quizService;
    public QuizzesController(IQuizService quizService)
    {
        this.quizService = quizService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(QuizCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.quizService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(QuizUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.quizService.ModifyAsync(dto)
        });
    
    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.quizService.DeleteAsync(id)
        });
    
    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.quizService.RetrieveAsync(id)
        });
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.quizService.RetrieveAllAsync()
        });
}