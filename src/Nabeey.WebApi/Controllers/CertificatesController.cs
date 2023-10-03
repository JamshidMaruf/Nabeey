using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Nabeey.Service.DTOs.Certificates;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class CertificatesController : BaseController
{
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly ICertificateService certificateService;
    private readonly IHttpContextAccessor httpContextAccessor;
    public CertificatesController(ICertificateService certificateService, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        this.certificateService = certificateService;
        this.webHostEnvironment = webHostEnvironment;
        this.httpContextAccessor = httpContextAccessor;
    }

    [AllowAnonymous]
    [HttpPost("generate")]
    public async ValueTask<IActionResult> Create(CertificateCreationDto dto)
    {
        var resultDto = await certificateService.GenerateAsync(dto);
        resultDto.File.FilePath = Path.Combine(webHostEnvironment.WebRootPath, resultDto.File.FilePath);
        return Ok(resultDto);
    }

    [AllowAnonymous]
    [HttpPost("getById/{id:long}")]
    public async ValueTask<IActionResult> GetById(long id)
    {   
        var resultDto = await certificateService.RetrieveByIdAsync(id);
        resultDto.File.FilePath = Path.Combine(webHostEnvironment.WebRootPath, resultDto.File.FilePath);
        return Ok(resultDto);
    }

    [AllowAnonymous]
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

    [AllowAnonymous]
    [HttpGet("api/items/myaction")]
    public IActionResult MyAction()
    {
        // HttpContext obyektini olish
        var httpContext = httpContextAccessor.HttpContext;

        string url = "/api/items/myaction";
        return Ok(new { message = "Item URL: " + url });
    }

    [AllowAnonymous]
    [HttpGet("api/items/myimage")]
    public IActionResult GetImage(string fileName)
    {
        // Faylni nomi (masalan, "my-image.jpg")

        // Faylni serverdan o'qish uchun yo'li
        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName+".jpg");

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound(); // Fayl topilmadi
        }

        // Faylni boshqa mediatip orqali qaytarish
        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "image/jpeg");
    }
}
