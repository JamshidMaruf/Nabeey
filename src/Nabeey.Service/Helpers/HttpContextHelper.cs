using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.Helpers;

public static class HttpContextHelper
{
    private static readonly IHttpContextAccessor httpContextAccessor;
    
    private static readonly HttpContext context = httpContextAccessor?.HttpContext;
    public static HttpContext HttpContext => httpContextAccessor?.HttpContext;
    public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
    public static long GetUserId => long.Parse(context?.User?.Claims.FirstOrDefault(claim => claim.Type == "Id").Value);
}