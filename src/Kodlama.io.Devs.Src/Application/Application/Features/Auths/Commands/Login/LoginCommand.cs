using Application.Features.Developers.Dtos;
using Application.Features.Developers.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Dtos;
using Core.Security.Encryption;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommand:IRequest<LoggedUserDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }

        

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedUserDto>
        {
            private readonly IDeveloperRepository developerRepository;
            private readonly IMapper mapper;
            private readonly ITokenHelper tokenHelper;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly IAuthService authService;
            public LoginCommandHandler(IDeveloperRepository developerRepository, IMapper mapper,ITokenHelper tokenHelper,AuthBussinessRules authBussinessRules,IAuthService authService)
            {
                this.authService = authService;
                this.developerRepository = developerRepository;
                this.mapper = mapper;
                this.authBussinessRules = authBussinessRules;
                this.tokenHelper = tokenHelper;
            }

            public async Task<LoggedUserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                await authBussinessRules.IsDeveloperExist(request.UserForLoginDto.Email);

                var developer = await developerRepository.GetAsync(x => x.Email == request.UserForLoginDto.Email);
                

               var result = HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password,developer.PasswordHash,developer.PasswordSalt);
                if (!result)
                    return null;

                AccessToken accessToken = await authService.CreateAccessToken(developer);

                LoggedUserDto loggedUserDto = mapper.Map<LoggedUserDto>(accessToken);

                return loggedUserDto;
                

                


            }
        }

    }
}
