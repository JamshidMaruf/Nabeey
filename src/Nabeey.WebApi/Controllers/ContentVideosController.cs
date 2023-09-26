using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.ContentVideos;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class ContentVideosController : BaseController
{
    private readonly IContentVideoService contentVideoService;
    public ContentVideosController(IContentVideoService contentVideoService)
    {
        this.contentVideoService = contentVideoService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(ContentVideoCreationDto dto)
           => Ok(new Response
           {
               StatusCode = 200,
               Message = "Success",
               Data = await this.contentVideoService.AddAsync(dto)
           });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await this.contentVideoService.RemoveAsync(id)
       });

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
      => Ok(new Response
      {
          StatusCode = 200,
          Message = "Success",
          Data = await this.contentVideoService.RetrieveByIdAsync(id)
      });

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetByContentIdAsync(long contentId)
      => Ok(new Response
      {
          StatusCode = 200,
          Message = "Success",
          Data = await this.contentVideoService.RetrieveAllByContentIdAsync(contentId)
      });

    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter, string search)
      => Ok(new Response
      {
          StatusCode = 200,
          Message = "Success",
          Data = await this.contentVideoService.RetrieveAsync(@params, filter, search)
      });
}