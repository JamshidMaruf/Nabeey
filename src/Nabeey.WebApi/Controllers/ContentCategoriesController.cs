﻿using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.DTOs.ContentCategories;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class ContentCategoriesController : BaseController
{
    private readonly IContentCategoryService contentCategoryService;
    public ContentCategoriesController(IContentCategoryService contentCategoryService)
    {
        this.contentCategoryService = contentCategoryService;
    }

    [HttpPost("create")]
    public async ValueTask<IActionResult> PostAsync(ContentCategoryCreationDto dto)
        => Ok(new Response
        {
            Status = 200,
            Message = "Success",
            Data = await this.contentCategoryService.AddAsync(dto)
        });


    [HttpPut("update")]
    public async ValueTask<IActionResult> PutAsync(ContentCategoryUpdateDto dto)
       => Ok(new Response
       {
           Status = 200,
           Message = "Success",
           Data = await this.contentCategoryService.ModifyAsync(dto)
       });


    [HttpDelete("delete/{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
       => Ok(new Response
       {
           Status = 200,
           Message = "Success",
           Data = await this.contentCategoryService.RemoveAsync(id)
       });


    [HttpGet("get/{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
       => Ok(new Response
       {
           Status = 200,
           Message = "Success",
           Data = await this.contentCategoryService.RetrieveByIdAsync(id)
       });


    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetAllAsync()
       => Ok(new Response
       {
           Status = 200,
           Message = "Success",
           Data = await this.contentCategoryService.RetrieveAllAsync()
       });
}
