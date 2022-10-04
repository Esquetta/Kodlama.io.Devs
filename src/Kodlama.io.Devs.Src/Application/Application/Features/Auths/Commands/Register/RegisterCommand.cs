using Application.Features.Auths.Dtos;
using Application.Features.Developers.Rules;
using Application.Services.Repositories;
using Core.Security.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommand : IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }

        public string IpAddress { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
        {
            private readonly AuthBussinessRules authBussinessRules;
            private readonly IUserRepository userRepository;
            public RegisterCommandHandler(AuthBussinessRules authBussinessRules,IUserRepository userRepository)
            {
                this.authBussinessRules = authBussinessRules;
                this.userRepository = userRepository;
            }

            
            public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
