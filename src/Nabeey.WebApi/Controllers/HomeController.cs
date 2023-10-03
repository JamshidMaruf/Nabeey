using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Net;
using Microsoft.Extensions.Options;
using Nabeey.WebApi.Models;

namespace Nabeey.WebApi.Controllers;

public class HomeController : Controller
{
    private readonly IOptions<AppSettings> appSettings;
    public HomeController(IOptions<AppSettings> appSettings)
    {
        this.appSettings = appSettings;
    }

    [HttpGet("")]
    public IActionResult GetLocalIPAddress()
    {
        var hostName = Dns.GetHostName();
        var addresses = Dns.GetHostAddresses(hostName);

        foreach (var address in addresses)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                return Ok(address.ToString());
            }
        }

        return Ok("No IP Address Found");
    }


    [HttpGet("get")]
    public IActionResult GetServerUrl()
    {
        return Ok(appSettings.Value.ApiBaseUrl);
    }

}
