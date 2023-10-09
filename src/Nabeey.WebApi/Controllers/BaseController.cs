using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nabeey.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]

public class BaseController : ControllerBase
{
}
