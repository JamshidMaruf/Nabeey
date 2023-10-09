﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.QuizQuestions;
using Nabeey.Service.Interfaces;
using Nabeey.Web.Models;

namespace Nabeey.Web.Controllers;

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

	[AllowAnonymous]
	[HttpGet("get/{id:long}")]
	public async ValueTask<IActionResult> GetAsync(long id)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.RetrieveAsync(id)
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
			Data = await this.service.RetrieveAllAsync(@params, filter, search)
		});

	[AllowAnonymous]
	[HttpGet("get-by-quizId/{quizId:long}")]
	public async ValueTask<IActionResult> GetByQuizAsync(long quizId)
		=> Ok(new Response
		{
			StatusCode = 200,
			Message = "Success",
			Data = await this.service.RetrieveAllByQuizIdAsync(quizId)
		});
}