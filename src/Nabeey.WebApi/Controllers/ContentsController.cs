using Nabeey.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Configurations;

namespace Nabeey.WebApi.Controllers;

public class ContentsController : BaseController
{
    private readonly IContentService contentService;
    public ContentsController(IContentService contentService)
    {
        this.contentService = contentService;
    }

    [HttpGet("get/{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.contentService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter, string search)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.contentService.RetrieveAllAsnyc(@params, filter, search)
        });
}