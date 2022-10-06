using Application.Features.Developers.Dtos;
using Core.Security.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Login
{
    internal class LoginCommand:IRequest<LoggedUserDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }

        public class LoginCommandHandler

    }
}
