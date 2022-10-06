using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Dtos;
using Application.Features.Developers.Commands.LoginDeveloper;
using Application.Features.Developers.Commands.RegisterDeveloper;
using Application.Features.Developers.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new() {
                UserForRegisterDto = userForRegisterDto,
                IpAddress=GetIpAddress()
            };
            
            RegisteredDto result = await Mediator.Send(registerCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);



        }
        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() {
            Expires=DateTime.Now.AddDays(7),
            HttpOnly=true
            };
            Response.Cookies.Append("RefreshToken",refreshToken.Token,cookieOptions);
        }
        

    }
}
