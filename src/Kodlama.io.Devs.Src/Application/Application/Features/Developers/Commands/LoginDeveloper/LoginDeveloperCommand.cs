using Application.Features.Developers.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.LoginDeveloper
{
    public class LoginDeveloperCommand : IRequest<LoggedUserDto>
    {
        public UserForLoginDto userForLoginDto { get; set; }
        public class LoginDeveloperCommandHandler : IRequestHandler<LoginDeveloperCommand, LoggedUserDto>
        {

            private readonly IDeveloperRepository developerRepository;
            private readonly ITokenHelper tokenHelper;
            private readonly IMapper mapper;
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IOperationClaimRepository operationClaimRepository;
            public LoginDeveloperCommandHandler(IDeveloperRepository developerRepository, ITokenHelper tokenHelper, IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, IOperationClaimRepository operationClaimRepository)
            {
                this.developerRepository = developerRepository;
                this.tokenHelper = tokenHelper;
                this.mapper = mapper;
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.operationClaimRepository = operationClaimRepository;
            }

            public async Task<LoggedUserDto> Handle(LoginDeveloperCommand request, CancellationToken cancellationToken)
            {


                Developer loggingDeveloper = await developerRepository.GetAsync(x => x.Email == request.userForLoginDto.Email);
                if (loggingDeveloper == null)
                {
                    return null;
                }

                var result = HashingHelper.VerifyPasswordHash(request.userForLoginDto.Password, loggingDeveloper.PasswordHash, loggingDeveloper.PasswordSalt);
                if (result == false)
                    return null;

                List<OperationClaim> claims = developerRepository.GetOperationClaims(loggingDeveloper);

                AccessToken token = tokenHelper.CreateToken(loggingDeveloper, claims);

                LoggedUserDto mappedToken = mapper.Map<LoggedUserDto>(token);

                return mappedToken;
            }
        }
    }
}
