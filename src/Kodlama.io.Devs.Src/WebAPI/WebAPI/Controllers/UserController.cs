using Application.Features.Developers.Commands.LoginDeveloper;
using Application.Features.Developers.Commands.RegisterDeveloper;
using Application.Features.Developers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDeveloperCommand registerDeveloperCommand)
        {
            RegisteredUserDto result = await Mediator.Send(registerDeveloperCommand);

            return Ok("You can login right now." + result.FirstName + " " + result.LastName);
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDeveloperCommand loginDeveloperCommand)
        {
            LoggedUserDto loggedUserDto = await Mediator.Send(loginDeveloperCommand);
            if (loggedUserDto != null)
                return Ok(loggedUserDto);
            return BadRequest("Invlaid Password or email");
        }

    }
}
