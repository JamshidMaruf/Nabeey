﻿using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Entities.QuestionAnswers;
using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

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
    public async ValueTask<IActionResult> UpdateAsync(QuestionUpdateDto dto)
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

    [HttpGet("get/{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.questionService.RetrieveByIdAsync(id)
        });

}
