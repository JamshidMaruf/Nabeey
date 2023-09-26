using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.ContentAudios;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class ContentAudiosController : BaseController
{
	private readonly IContentAudioService contentAudioService;
	public ContentAudiosController(IContentAudioService contentAudioService)
	{
		this.contentAudioService = contentAudioService;
	}

	[HttpPost("create")]
	public async ValueTask<IActionResult> PostAsync(ContentAudioCreationDto dto)
	   => Ok(new Response
	   {
		   StatusCode = 200,
		   Message = "Success",
		   Data = await this.contentAudioService.AddAsync(dto)
	   });

	[HttpDelete("delete/{id:long}")]
	public async ValueTask<IActionResult> DeleteAsync(long id)
	   => Ok(new Response
	   {
		   StatusCode = 200,
		   Message = "Success",
		   Data = await this.contentAudioService.RemoveAsync(id)
	   });

	[HttpGet("get/{id:long}")]
	public async ValueTask<IActionResult> GetAsync(long id)
	  => Ok(new Response
	  {
		  StatusCode = 200,
		  Message = "Success",
		  Data = await this.contentAudioService.RetrieveByIdAsync(id)
	  });

	[HttpGet("get-all")]
	public async ValueTask<IActionResult> GetAllAsync(
		[FromQuery] PaginationParams @params,
		[FromQuery] Filter filter, string search)
	  => Ok(new Response
	  {
		  StatusCode = 200,
		  Message = "Success",
		  Data = await this.contentAudioService.RetrieveAsync(@params, filter, search)
	  });
}