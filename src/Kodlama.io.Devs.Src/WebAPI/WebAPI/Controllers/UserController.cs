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

    }
}
