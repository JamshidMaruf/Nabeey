using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.DTOs.Certificates;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class CertificatesController : BaseController
{
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly ICertificateService certificateService;
    public CertificatesController(ICertificateService certificateService, IWebHostEnvironment webHostEnvironment)
    {
        this.certificateService = certificateService;
        this.webHostEnvironment = webHostEnvironment;
    }

    [HttpPost("generate")]
    public async ValueTask<IActionResult> Create(CertificateCreationDto dto)
    {
        var resultDto = await certificateService.GenerateAsync(dto);
        resultDto.File.FilePath = Path.Combine(webHostEnvironment.WebRootPath, resultDto.File.FilePath);
        return Ok(resultDto);
    }

    [HttpPost("getById/{id:long}")]
    public async ValueTask<IActionResult> GetById(long id)
    {   
        var resultDto = await certificateService.RetrieveByIdAsync(id);
        resultDto.File.FilePath = Path.Combine(webHostEnvironment.WebRootPath, resultDto.File.FilePath);
        return Ok(resultDto);
    }

    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetCertificate()
    {
        var dtos = await certificateService.RetrieveAllAsync();

        foreach (var i in dtos)
            i.File.FilePath = Path.Combine(webHostEnvironment.WebRootPath, i.File.FilePath);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = dtos
        });
    }
}
