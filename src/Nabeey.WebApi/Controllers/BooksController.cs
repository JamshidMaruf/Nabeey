using Microsoft.AspNetCore.Mvc;
using Nabeey.Domain.Configurations;
using Nabeey.Service.DTOs.Books;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class BooksController : BaseController
{
    private readonly IBookService bookService;
    public BooksController(IBookService bookService)
    {
        this.bookService = bookService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromQuery]BookCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromQuery]BookUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.DeleteAsync(id)
        });

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync(
		[FromQuery] PaginationParams @params,
		[FromQuery] Filter filter, string search)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookService.RetrieveAllAsync(@params,search)
        });

}
