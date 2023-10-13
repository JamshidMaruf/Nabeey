using Microsoft.AspNetCore.Mvc;
using Nabeey.Service.DTOs.Users;
using Nabeey.Service.Interfaces;

namespace Nabeey.Web.Controllers;

public class UsersController : Controller
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    public async ValueTask<IActionResult> Index()
        => View(await userService.RetrieveAllAsync());


    public async ValueTask<IActionResult> Details(long id)
        => View(await userService.RetrieveByIdAsync(id));

    public async ValueTask<IActionResult> Update(long id)
        => View(await userService.RetrieveByIdAsync(id));

    [HttpPut]
    public async ValueTask<IActionResult> Update(UserUpdateDto dto)
    {
        await userService.ModifyAsync(dto);
        return Redirect("/index");
    }



    [HttpPost]
    public async ValueTask<IActionResult> Delete(long id)
    {
        var i = id;
        await userService.RemoveAsync(id);
        return Redirect("index");
    }

    public IActionResult Create()
        => View();

    [HttpPost]
    public async ValueTask<IActionResult> Create(UserCreationDto dto)
    {
        await userService.AddAsync(dto);
        return Redirect("/index");
    }

}
