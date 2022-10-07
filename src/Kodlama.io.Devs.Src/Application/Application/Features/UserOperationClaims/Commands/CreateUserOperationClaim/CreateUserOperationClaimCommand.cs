using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand : IRequest<CreatedUserOperationClaimDto>, ISecuredRequest
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public string[] Roles { get; } = {"Admin"};

        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimDto>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules userOperationClaimBusinessRules;
            public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper,UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                this.userOperationClaimBusinessRules = userOperationClaimBusinessRules;
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.mapper = mapper;
            }

            public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await userOperationClaimBusinessRules.IsOperationExist(request.OperationClaimId);
                await userOperationClaimBusinessRules.IsUserExist(request.UserId);


                UserOperationClaim userOperationClaim = mapper.Map<UserOperationClaim>(request);

                UserOperationClaim createdUserOperationClaim = await userOperationClaimRepository.AddAsync(userOperationClaim);

                CreatedUserOperationClaimDto createdUserOperationClaimDto = mapper.Map<CreatedUserOperationClaimDto>(createdUserOperationClaim);

                return createdUserOperationClaimDto;


            }
        }
    }
}
