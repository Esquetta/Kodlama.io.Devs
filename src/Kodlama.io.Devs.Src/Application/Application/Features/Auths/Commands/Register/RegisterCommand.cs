using Application.Features.Auths.Dtos;
using Application.Features.Developers.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
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
            private readonly AuthBusinessRules authBussinessRules;
            private readonly IDeveloperRepository developerRepository;
            private readonly IAuthService authService;
            private readonly IMapper mapper;
            public RegisterCommandHandler(AuthBusinessRules authBussinessRules, IDeveloperRepository developerRepository, IAuthService authService,IMapper mapper )
            {
                this.authBussinessRules = authBussinessRules;
                this.developerRepository = developerRepository;
                this.authService = authService;
                this.mapper = mapper;
            }

            
            public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await authBussinessRules.IsDeveloperExist(request.UserForRegisterDto.Email);
                byte[] passwordHash, passwordSalt;

                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password,out passwordHash,out passwordSalt);
                Developer mappedDeveloper = mapper.Map<Developer>(request.UserForRegisterDto);
                mappedDeveloper.PasswordHash = passwordHash;
                mappedDeveloper.PasswordSalt = passwordSalt;

                Developer addedDeveloper  = await developerRepository.AddAsync(mappedDeveloper);


                AccessToken accessToken = await authService.CreateAccessToken(addedDeveloper);
                RefreshToken createdRefreshToken = await authService.CreateRefreshToken(addedDeveloper, request.IpAddress);


                RefreshToken addedRefreshToken = await authService.AddRefreshToken(createdRefreshToken);


                RegisteredDto registeredDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = accessToken
                };

                return registeredDto;



                

            }
        }
    }
}
