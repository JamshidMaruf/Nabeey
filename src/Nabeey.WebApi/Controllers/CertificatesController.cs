using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Assets;
using Nabeey.Service.DTOs.Certificates;
using Nabeey.Service.Helpers;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class CertificatesController : BaseController
{
    private readonly ICertificateService certificateService;
    public CertificatesController(ICertificateService certificateService)
    {
        this.certificateService = certificateService;
    }

    [AllowAnonymous]
    [HttpPost("generate")]
    public async ValueTask<IActionResult> Create(CertificateCreationDto dto)
     => Ok(new Response
     {
         StatusCode = 200,
         Message = "Success",
         Data = await certificateService.GenerateAsync(dto)
     });

    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async ValueTask<IActionResult> GetById(long id)
     => Ok(new Response
     {
         StatusCode = 200,
         Message = "Success",
         Data = await certificateService.RetrieveByIdAsync(id)
     });

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async ValueTask<IActionResult> GetCertificate()
     => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await certificateService.RetrieveAllAsync()
        });
}
