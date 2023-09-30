using Microsoft.AspNetCore.Http;

namespace Nabeey.Service.Helpers;

public static class HttpContextHelper
{
/*	private static readonly IHttpContextAccessor httpContextAccessor;

	private static readonly HttpContext context = httpContextAccessor?.HttpContext;
	public static HttpContext HttpContext => httpContextAccessor?.HttpContext;
	public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
	public static long? GetUserId => long.TryParse(HttpContext?.User?.FindFirst("id")?.Value, out _tempUserId) ? _tempUserId : null;
	public static string UserRole => HttpContext?.User?.FindFirst("role")?.Value;

	private static long _tempUserId;*/

	public static IHttpContextAccessor Accessor { get; set; }
    public static HttpContext HttpContext => Accessor?.HttpContext;
    public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
    public static long? GetUserId => long.TryParse(HttpContext?.User?.FindFirst("id")?.Value, out _tempUserId) ? _tempUserId : null;
    public static string UserRole => HttpContext?.User?.FindFirst("role")?.Value;

    private static long _tempUserId;
}