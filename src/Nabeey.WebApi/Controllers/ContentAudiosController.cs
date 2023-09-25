using Nabeey.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.Interfaces;
using Nabeey.Service.DTOs.ContentAudios;

namespace Nabeey.WebApi.Controllers
{
    public class ContentAudiosController : BaseController
    {
        private readonly IContentAudioService contentAudioService;
        public ContentAudiosController(IContentAudioService contentAudioService)
        {
            this.contentAudioService = contentAudioService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostAsync(ContentAudioCreationDto dto)
           => Ok(new Response
           {
               Status = 200,
               Message = "Success",
               Data = await this.contentAudioService.UploadAsync(dto)
           });

        [HttpDelete("delete/{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
           => Ok(new Response
           {
               Status = 200,
               Message = "Success",
               Data = await this.contentAudioService.RemoveAsync(id)
           });

        [HttpGet("get/{id:long}")]
        public async Task<IActionResult> GetAsync(long id)
          => Ok(new Response
          {
              Status = 200,
              Message = "Success",
              Data = await this.contentAudioService.RetrieveByIdAsync(id)
          });
    }
}