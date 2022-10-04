using Application.Features.Developers.Dtos;
using Application.Features.Developers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.RegisterDeveloper
{
    public class RegisterDeveloperCommand : IRequest<RegisteredUserDto>
    {
        public UserForRegisterDto RegisterDto { get; set; }

        public class RegisterDeveloperCommandHandler : IRequestHandler<RegisterDeveloperCommand, RegisteredUserDto>
        {
            private readonly IDeveloperRepository developerRepository;
            private readonly IMapper mapper;
            private readonly AuthBussinessRules developerBussinessRules;
            
            private readonly IOperationClaimRepository operationClaimRepository;
            private readonly IUserOperationClaimRepository userOperationClaimRepository;

            public RegisterDeveloperCommandHandler(IDeveloperRepository developerRepository, IMapper mapper, AuthBussinessRules developerBussinessRules,IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository)
            {
                this.developerRepository = developerRepository;
                this.mapper = mapper;
                
                this.developerBussinessRules = developerBussinessRules;
                this.operationClaimRepository = operationClaimRepository;
                this.userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<RegisteredUserDto> Handle(RegisterDeveloperCommand request, CancellationToken cancellationToken)
            {
                byte[] passwordHash, passwordSalt;
                OperationClaim createdOperationClaim = new OperationClaim();
                UserOperationClaim userOperationClaim = new UserOperationClaim();

                await developerBussinessRules.IsDeveloperExist(request.RegisterDto.Email);



                HashingHelper.CreatePasswordHash(request.RegisterDto.Password, out passwordHash, out passwordSalt);
                Developer developer = mapper.Map<Developer>(request.RegisterDto);
                developer.PasswordSalt = passwordSalt;
                developer.PasswordHash = passwordHash;
                developer.Status = true;
                developer.AuthenticatorType = AuthenticatorType.None;



                Developer registeredDeveloper = await developerRepository.AddAsync(developer);

                OperationClaim operationClaim = await operationClaimRepository.GetAsync(x => x.Name == "Developer");

                if (operationClaim == null)
                {
                    createdOperationClaim = await operationClaimRepository.AddAsync(new OperationClaim { Name = "Developer" });
                    userOperationClaim = await userOperationClaimRepository.AddAsync(new UserOperationClaim { UserId = registeredDeveloper.Id, OperationClaimId = createdOperationClaim.Id });
                }


                userOperationClaim = await userOperationClaimRepository.AddAsync(new UserOperationClaim { UserId = registeredDeveloper.Id, OperationClaimId = operationClaim.Id });


                

                RegisteredUserDto registeredUserDto = mapper.Map<RegisteredUserDto>(registeredDeveloper);

                return registeredUserDto;

            }
        }
    }
}
