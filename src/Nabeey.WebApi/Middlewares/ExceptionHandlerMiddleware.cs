using Nabeey.Service.Exceptions;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    public readonly RequestDelegate request;
    public readonly ILogger<ExceptionHandlerMiddleware> logger;
    public ExceptionHandlerMiddleware(RequestDelegate request, ILogger<ExceptionHandlerMiddleware> logger)
    {
        this.request = request;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this.request(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Status = context.Response.StatusCode,
                Message = ex.Message,
            });
        }
        catch (AlreadyExistException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Status = context.Response.StatusCode,
                Message = ex.Message,
            });
        }
        catch (CustomException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Status = context.Response.StatusCode,
                Message = ex.Message,
            });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            this.logger.LogError(ex.ToString());
            await context.Response.WriteAsJsonAsync(new Response
            {
                Status = context.Response.StatusCode,
                Message = ex.Message,
            });
        }
    }
}